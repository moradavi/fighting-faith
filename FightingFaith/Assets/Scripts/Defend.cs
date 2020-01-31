using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{
    
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
        timer += Time.deltaTime;
        //Debug.Log(timer);

        if(timer >= timeLimit)
        {
            Debug.Log("TIMEOUT");
            //ResetPattern();
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;

        if (Input.GetMouseButton(0))
        {
            Collider2D[] frameCol = Physics2D.OverlapPointAll(new Vector2(mousePos.x, mousePos.y), LayerMask.GetMask("Defend"));
            foreach (Collider2D col in frameCol)
            {
                if (pointsToHit.Contains(col.gameObject))
                {
                    for(int i = 0; i < pointsToHit.Count; i++)
                    {
                        if(pointsToHit[i] == col.gameObject)
                        {
                            defensePoints.Add(pointsToHit[i]);
                            pointsToHit.RemoveAt(i);

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
            GameObject randomPoint = defensePoints[1];

            pointsToHit.Add(randomPoint);
            defensePoints.RemoveAt(indexPoint);
        }
        
    }
    

}
