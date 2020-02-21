using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{
    //THIS SCRIPT IS NOT CURRENTLY FUNCTIONING AS NECESSARY.

    public int numOfPoints;

    public float timeLimit;
    float timer;
    
    public List<GameObject> defensePoints;
    public List<GameObject> pointsToHit;

    //public List<GameObject> pointsNotHit;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        ResetPattern();
        
    }

    // Update is called once per frame
    void Update()
    {
        //track the time
        timer += Time.deltaTime;
        //Debug.Log(timer);

        //if the timer runs out, trigger an event in the future
        if(timer >= timeLimit)
        {
            Debug.Log("TIMEOUT");
            //ResetPattern();
        }

        //get the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;

        //if the player is inputting, pressing on screen
        if (Input.GetMouseButton(0))
        {
            //and it hits a defense node
            Collider2D[] frameCol = Physics2D.OverlapPointAll(new Vector2(mousePos.x, mousePos.y), LayerMask.GetMask("Defend"));
            foreach (Collider2D col in frameCol)
            {
                //if it is a point that needs to be hit
                if (pointsToHit.Contains(col.gameObject))
                {
                    for(int i = 0; i < pointsToHit.Count; i++)
                    {
                        //count it as hit
                        if(pointsToHit[i] == col.gameObject)
                        {
                            defensePoints.Add(pointsToHit[i]);
                            pointsToHit.RemoveAt(i);

                            //if all of the points are hit, player has succeeded in defending
                            if(pointsToHit.Count == 0)
                            {
                                Debug.Log("Yay");
                                //ResetPattern();
                            }

                        }
                    }
                }
            }
        }

        //if the player does not finish the pattern, count as failed attempt
        else if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < pointsToHit.Count; i++)
            {
                defensePoints.Add(pointsToHit[i]);
                pointsToHit.RemoveAt(i);
            }

            Debug.Log("Failed Attempt");
            //ResetPattern();
        }
        

    }

    void ResetPattern()
    {
        for (int i = 0; i < numOfPoints; i++)
        {
            int indexPoint = Random.Range(0, defensePoints.Count);
            GameObject randomPoint = defensePoints[indexPoint];

            pointsToHit.Add(randomPoint);
            defensePoints.RemoveAt(indexPoint);
        }
        
    }
    

}
