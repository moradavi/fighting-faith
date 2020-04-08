using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Defend : MonoBehaviour
{
    //THIS SCRIPT IS NOT CURRENTLY FUNCTIONING AS NECESSARY.

    public int numOfPoints;

    public float timeLimit;
    public float extendedTime;
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

    public float timeSpacing;

    public int numPointsHit;

    public AudioSource successfulParrySound;
    public AudioSource failParrySound;

    //public List<GameObject> pointsNotHit;

    // Start is called before the first frame update
    void Start()
    {

        //HideDefendPoints();
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
                //pointsToHit.Clear();

                Debug.Log("TIMEOUT");
                if (redFlash.GetComponent<Animator>().enabled == false)
                {
                    redFlash.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    redFlash.GetComponent<Animator>().Play("anim_redFlash", -1, 0f);
                }
                enemy.FollowThrough();
                onDefendFail.Invoke();
                failParrySound.Play();
                EndDefend();
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
                        //for (int i = 0; i < pointsToHit.Count; i++)
                        //{
                            //count it as hit
                            if (pointsToHit[numPointsHit] == col.gameObject)
                            {
                            //defensePoints.Add(pointsToHit[i]);
                            //pointsToHit.RemoveAt(i);

                                col.gameObject.GetComponent<Animator>().SetBool("pointSelected", true);
                                numPointsHit++;
                                col.gameObject.GetComponent<Collider2D>().enabled = false;

                                //if all of the points are hit, player has succeeded in defending
                                //if (pointsToHit.Count == 0)
                                if (pointsToHit.Count == numPointsHit)
                                {
                                    Debug.Log("Yay");
                                    enemy.gameObject.GetComponentInChildren<FlashDamage>().FlashTrigger();
                                    enemy.animator.SetTrigger("Parry");
                                    onDefendSuccess.Invoke();
                                    successfulParrySound.Play();
                                    EndDefend();
                                    patternComplete = true;
                                    numPointsHit = 0;
                                }

                            }
                        else
                        {
                            if (redFlash.GetComponent<Animator>().enabled == false)
                            {
                                redFlash.GetComponent<Animator>().enabled = true;
                            }
                            else
                            {
                                redFlash.GetComponent<Animator>().Play("anim_redFlash", -1, 0f);
                            }
                            enemy.FollowThrough();
                            onDefendFail.Invoke();
                            failParrySound.Play();
                            EndDefend();
                        }

                        //}
                    }
                }

            }



            //if the player does not finish the pattern, count as failed attempt
            else if (Input.GetMouseButtonUp(0))
            {
                if (!patternComplete)
                {
                    numPointsHit = 0;

                    Debug.Log("Failed Attempt");

                    if(redFlash.GetComponent<Animator>().enabled == false)
                    {
                        redFlash.GetComponent<Animator>().enabled = true;
                    }
                    else
                    {
                        redFlash.GetComponent<Animator>().Play("anim_redFlash", -1, 0f);
                    }

                    enemy.FollowThrough();
                    onDefendFail.Invoke();
                    failParrySound.Play();
                    EndDefend();

                }

                else
                {
                    patternComplete = false;
                }

            }

            //Show only the points used for current defense phase
            //HideDefendPoints();

            //for (int i = 0; i < pointsToHit.Count; i++)
            //{
            //    pointsToHit[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;

            //}
        }
        
    }

    public void BeginDefend()
    {
        //Start defense and reset values/patterns
        patternComplete = false;
        ResetPattern();
        isDefending = true;
        swipeScript.isDefending = true;

        for ( int i = 0; i < pointsToHit.Count; i++)
        {
            //Invoke("FadeInPoints", i * timeSpacing * Time.deltaTime);

            StartCoroutine(FadeInPoints(pointsToHit[i], i * timeSpacing * Time.deltaTime));
        }
        

    }

    public void EndDefend()
    {
        //Signal defense is over
        isDefending = false;
        enemy.StopAttacking();
        swipeScript.isDefending = false;
        timer = 0;
        Debug.Log(pointsToHit.Count);
        for (int i = 0; i < pointsToHit.Count; i++)
        {
            Debug.Log("boop");
            StartCoroutine(FadeOutPoints(pointsToHit[i], i * timeSpacing * Time.deltaTime));
        }

        for (int i = 0; i < pointsToHit.Count; i++)
        {
            pointsToHit.Clear();
        }

        GetComponent<DrawConnectLine>().EraseLines();
    }

    void ResetPattern()
    {
        //Make points have all the defense points
        for (int i = 0; i < defensePoints.Count; i++)
        {
            points.Add(defensePoints[i]);
        }
        
        //Depending on the number of points necessary for defense,
        for (int i = 0; i < numOfPoints; i++)
        {
            //Find a random point in list
            int indexPoint = Random.Range(0, points.Count);
            GameObject randomPoint = points[indexPoint];

            //Make sure to count this as a point that cannot be duplicated
            duplicatePoints.Add(randomPoint);

            //Resort points list to only contain points not in duplicate list so next loop it won't accidentally chose it
            points.Clear();
            for( int x = 0; x < defensePoints.Count; x++)
            {
                if (!duplicatePoints.Contains(defensePoints[x]))
                {
                    points.Add(defensePoints[x]);
                }
            }

            //Add the random point to the hit points
            if(pointsToHit.Contains(randomPoint) == false)
            {
                pointsToHit.Add(randomPoint.gameObject);
            }

            
            //defensePoints.RemoveAt(indexPoint);
        }

        //Clear both temporary lists for next time pattern needs to be generated
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

    IEnumerator FadeInPoints(GameObject point, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if(isDefending == true)
        {
            if (point.gameObject.activeSelf == false)
            {
                point.gameObject.SetActive(true);
            }
            else
            {
                point.gameObject.GetComponent<Animator>().SetTrigger("pointFadeIn");
            }

            point.gameObject.GetComponent<Collider2D>().enabled = true;
        }
        

        yield return null;

    }

    IEnumerator FadeOutPoints(GameObject point, float waitTime)
    {
        yield return new WaitForSeconds(waitTime/2);

        point.gameObject.GetComponent<Animator>().SetTrigger("pointFadeOut");
        point.gameObject.GetComponent<Animator>().SetBool("pointSelected", false);

        yield return null;

    }

}
