using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaypointMovement : MonoBehaviour
{
    //Movement Values
    public float waypointPauseTime;
    public float speed;

    //Float Lerp Values
    float lerpTime;
    float currentLerpTime;
    float currentLerpTimer;
    float currentlerpDistance;

    //Waypoint Data
    public List<Transform> waypoints = new List<Transform>();
    public int startWaypointIndex;
    Transform targetWaypoint;
    Transform currentWaypoint;
    Transform homeWaypoint;
    public Transform attackWaypoint;
    int targetWaypointIndex;

    //Bool Properties
    public bool IsMoving { get; private set; }
    public bool IsStopped { get; private set; }
    bool hasArrived;

    //Events
    public UnityEvent onTargetArrive;
    public UnityEvent onTargetLeave;
    public UnityEvent onArriveAtAttackWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        //Set intial target waypoint
        IsStopped = false;
        homeWaypoint = waypoints[startWaypointIndex];
        transform.position = homeWaypoint.position;
        targetWaypointIndex = startWaypointIndex;
        SetNewRandomTargetWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        //Move towards the target waypoint
        if (IsMoving)
        {
            //transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, Time.deltaTime * speed);
            LerpTowardsTarget();
        }
            

        //If arrived at the target waypoint
        if ((transform.position == targetWaypoint.position) && IsMoving == true)
        {
            //Check if the target is the attack waypoint
            if(targetWaypoint == attackWaypoint)
            {
                currentWaypoint = targetWaypoint;
                homeWaypoint = targetWaypoint;
                IsMoving = false;
                onArriveAtAttackWaypoint.Invoke();               
            }
            else
            {
                //Pause for set amount of time, then move to next waypoint
                onTargetArrive.Invoke();
                currentWaypoint = targetWaypoint;
                homeWaypoint = targetWaypoint;
                IsMoving = false;
                Invoke("SetNewRandomTargetWaypoint", waypointPauseTime);
            }
           
        }
    }

    private void LerpTowardsTarget()
    {
        currentLerpTimer += Time.deltaTime;           
        if(currentLerpTimer > currentLerpTime)
        {
            currentLerpTimer = currentLerpTime;
        }
 
        float perc = currentLerpTimer / currentLerpTime;
        perc = perc * perc * (3f - 2f * perc);
        transform.position = Vector2.Lerp(homeWaypoint.position, targetWaypoint.position, perc);

    }

    private void SetLerpValues(Vector3 start, Vector3 target)
    {
        currentlerpDistance = Vector2.Distance(homeWaypoint.position, targetWaypoint.position);
        currentLerpTime = currentlerpDistance / speed;
    }

    private void ResetLerpValues()
    {
        currentLerpTimer = 0;
    }

    public void StopWaypointMovement()
    {
        if (!IsStopped)
        {
            IsMoving = false;
            IsStopped = true;
            
            //Cancels movement invokes
            CancelInvoke();
        }       
    }

    public void ResumeWaypointMovement()
    {
        if (IsStopped)
        {
            IsMoving = true;
            IsStopped = false;
        }      
    }

    public void SetNewRandomTargetWaypoint()
    {
        //Sets a new waypoint with the exception of the current waypoint
        targetWaypointIndex = RandomExcept(0, waypoints.Count, targetWaypointIndex);
        SetTargetWaypoint(waypoints[targetWaypointIndex]);
    }

    void SetTargetWaypoint(Transform waypoint)
    {
        ResetLerpValues();        
        targetWaypoint = waypoint;
        currentWaypoint = null;
        SetLerpValues(homeWaypoint.position, targetWaypoint.position);
        IsMoving = true;

        if(waypoint != attackWaypoint)
            onTargetLeave.Invoke();       
    }

    public void GoToAttackWaypoint()
    {
        SetTargetWaypoint(attackWaypoint);
    }

    //Returns a random interger within a range with the expection of the provided int
    public int RandomExcept(int min, int max, int except)
    {
        int random = Random.Range(min, max);
        if (random >= except) random = (random + 1) % max;
        return random;
    }

}
