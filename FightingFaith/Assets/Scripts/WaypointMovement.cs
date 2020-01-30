using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaypointMovement : MonoBehaviour
{  
    public List<Transform> waypoints = new List<Transform>();
    public float waypointPauseTime;
    public float speed;

    Transform targetWaypoint;
    Transform currentWaypoint;
    int targetWaypointIndex;
    
    public bool IsMoving { get; private set; }
    public bool IsStopped { get; private set; }
    public UnityEvent onTargetArrive;

    // Start is called before the first frame update
    void Start()
    {
        //Set intial target waypoint
        IsStopped = false;
        targetWaypointIndex = Random.Range(0, waypoints.Count);
        SetTargetWaypoint(waypoints[targetWaypointIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        //Move towards the target waypoint
        if (IsMoving)
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, Time.deltaTime * speed);

        //If arrived at the target waypoint
        if ((transform.position == targetWaypoint.position) && IsMoving == true)
        {
            //Pause for set amount of time, then move to next waypoint
            onTargetArrive.Invoke();
            currentWaypoint = targetWaypoint;
            IsMoving = false;
            Invoke("SetNewRandomTargetWaypoint", waypointPauseTime);
        }
    }

    public void StopWaypointMovement()
    {
        if (!IsStopped)
        {
            IsMoving = false;
            IsStopped = true;
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

    void SetNewRandomTargetWaypoint()
    {
        targetWaypointIndex = RandomExcept(0, waypoints.Count, targetWaypointIndex);
        SetTargetWaypoint(waypoints[targetWaypointIndex]);
    }

    void SetTargetWaypoint(Transform waypoint)
    {
        targetWaypoint = waypoint;
        currentWaypoint = null;
        IsMoving = true;
    }

    public int RandomExcept(int min, int max, int except)
    {
        int random = Random.Range(min, max);
        if (random >= except) random = (random + 1) % max;
        return random;
    }

}
