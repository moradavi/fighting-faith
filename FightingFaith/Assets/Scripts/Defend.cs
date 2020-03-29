﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Defend : MonoBehaviour
{
    //THIS SCRIPT IS NOT CURRENTLY FUNCTIONING AS NECESSARY.

    public int numOfPoints;

    public float timeLimit;
    float timer;
    
    public List<GameObject> defensePoints;
    public List<GameObject> pointsToHit;

    public List<GameObject> points;
    public List<GameObject> duplicatePoints;

    public bool patternComplete;

    public Enemy enemy;
    public Swipe2 swipeScript;
    public bool isDefending;

    public UnityEvent onDefendFail;
    public UnityEvent onDefendSuccess;

    public GameObject redFlash;

    //public List<GameObject> pointsNotHit;

    // Start is called before the first frame update
    void Start()
    {

        HideDefendPoints();
        timer = 0;
        enemy.onAttack.AddListener(BeginDefend);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefending)
        {
            //track the time
            timer += Time.deltaTime;
            //Debug.Log(timer);

            //if the timer runs out, trigger an event in the future
            if (timer >= timeLimit)
            {
                Debug.Log("TIMEOUT");
            }

            //get the mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -1;

            //if the player is inputting, pressing on screen
            if (Input.GetMouseButton(0) && !patternComplete)
            {
                //and it hits a defense node
                Collider2D[] frameCol = Physics2D.OverlapPointAll(new Vector2(mousePos.x, mousePos.y), LayerMask.GetMask("Defend"));
                foreach (Collider2D col in frameCol)
                {
                    //if it is a point that needs to be hit
                    if (pointsToHit.Contains(col.gameObject))
                    {
                        for (int i = 0; i < pointsToHit.Count; i++)
                        {
                            //count it as hit
                            if (pointsToHit[i] == col.gameObject)
                            {
                                //defensePoints.Add(pointsToHit[i]);
                                pointsToHit.RemoveAt(i);

                                //if all of the points are hit, player has succeeded in defending
                                if (pointsToHit.Count == 0)
                                {
                                    Debug.Log("Yay");
                                    enemy.gameObject.GetComponentInChildren<FlashDamage>().FlashTrigger();
                                    onDefendSuccess.Invoke();
                                    EndDefend();
                                    patternComplete = true;
                                }

                            }
                        }
                    }
                }

            }



            //if the player does not finish the pattern, count as failed attempt
            else if (Input.GetMouseButtonUp(0))
            {
                if (!patternComplete)
                {
                    for (int i = 0; i < pointsToHit.Count; i++)
                    {
                        pointsToHit.Clear();
                    }

                    Debug.Log("Failed Attempt");
                    redFlash.GetComponent<Animator>().enabled = true;
                    onDefendFail.Invoke();
                    EndDefend();

                }

                else
                {
                    patternComplete = false;
                }

            }


            HideDefendPoints();

            for (int i = 0; i < pointsToHit.Count; i++)
            {
                pointsToHit[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;

            }
        }
        
    }

    public void BeginDefend()
    {
        patternComplete = false;
        ResetPattern();
        isDefending = true;
        swipeScript.isDefending = true;
    }

    public void EndDefend()
    {
        
        isDefending = false;
        enemy.StopAttacking();
        swipeScript.isDefending = false;
    }

    void ResetPattern()
    {
        for (int i = 0; i < defensePoints.Count; i++)
        {
            points.Add(defensePoints[i]);
        }
        

        for (int i = 0; i < numOfPoints; i++)
        {
            int indexPoint = Random.Range(0, points.Count);
            GameObject randomPoint = points[indexPoint];

            duplicatePoints.Add(randomPoint);

            points.Clear();
            for( int x = 0; x < defensePoints.Count; x++)
            {
                if (!duplicatePoints.Contains(defensePoints[x]))
                {
                    points.Add(defensePoints[x]);
                }
            }

            if(pointsToHit.Contains(randomPoint) == false)
            {
                pointsToHit.Add(randomPoint.gameObject);
            }

            
            //defensePoints.RemoveAt(indexPoint);
        }

        duplicatePoints.Clear();
        points.Clear();        
    }
    
    void HideDefendPoints()
    {
        for (int i = 0; i < defensePoints.Count; i++)
        {
            defensePoints[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }



}
