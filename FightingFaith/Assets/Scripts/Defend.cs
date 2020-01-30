using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{
    
    public int numOfPoints;

    public float timeLimit;
    float timer;

    public List<GameObject> allPoints;

    public List<GameObject> defensePoints;
    public List<GameObject> pointsToHit;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        
        defensePoints = allPoints;
        
        for( int i = 0; i < numOfPoints; i++)
        {
            int indexPoint = Random.Range(0, defensePoints.Count - 1);
            GameObject randomPoint = defensePoints[indexPoint];

            pointsToHit.Add(randomPoint);
            defensePoints.RemoveAt(indexPoint);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeLimit)
        {
            Debug.Log("GAME OVER");
        }


        //if (Input.GetMouseButton(0))
        //{

        //}


    }
}
