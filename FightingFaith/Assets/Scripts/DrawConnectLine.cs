﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConnectLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    Defend defend;
    
    // Start is called before the first frame update
    void Start()
    {
        defend = GetComponent<Defend>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is defending
        if (defend.isDefending)
        {
            if (defend.numPointsHit > 0)
            {
                //make the number of segments equal the number of points + one for the mouse
                lineRenderer.positionCount = defend.numPointsHit + 1;

                for (int i = 0; i < defend.numPointsHit; i++)
                {
                    if (defend.pointsToHit.Count != 0)
                    {
                        lineRenderer.SetPosition(i, defend.pointsToHit[i].transform.position);
                    }
                }

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = -1;

                //set the last position to be the mouse
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
            }
        }      
    }

    public void EraseLines()
    {
        Invoke("Erased", 1f);
    }

    void Erased()
    {
        lineRenderer.positionCount = 0;
    }
}
