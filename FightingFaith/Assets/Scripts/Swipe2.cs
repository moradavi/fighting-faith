using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe2 : MonoBehaviour
{
    Vector3 lastMousePosition;
    public float swipeSpeedThreshold;

    public GameObject mouseSprite;
    public bool isDefending;

    public int attackStrength;
    public GameObject player;
    public GameObject defend;

    // Update is called once per frame
    void Update()
    {
        //Get the position of the mouse on the screen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;
        
        //If the player is pressing down input 
        if (Input.GetMouseButton(0))
        {
            mouseSprite.gameObject.transform.position = mousePos;

            if (!isDefending)
            {
                //Check if there is a collider from a gameobject that's an Enemy
                Collider2D[] frameCol = Physics2D.OverlapPointAll(new Vector2(mousePos.x, mousePos.y), LayerMask.GetMask("Enemy"));
                if ((Input.mousePosition - lastMousePosition).sqrMagnitude > swipeSpeedThreshold)
                {
                    //if there is, deal damage and flash the enemy
                    foreach (Collider2D col in frameCol)
                    {
                        Debug.Log("hit");

                        //if the player is at minimum health, amp up time and attack strength of player.
                        if(player.GetComponent<Health>().CurrentHealth <= 1)
                        {
                            col.gameObject.GetComponent<Health>().LoseHealth(attackStrength);
                        }
                        else
                        {
                            col.gameObject.GetComponent<Health>().LoseHealth(attackStrength*3);
                            defend.GetComponent<Defend>().timeLimit = defend.GetComponent<Defend>().extendedTime;
                        }
                        
                        col.gameObject.GetComponentInChildren<FlashDamage>().FlashTrigger();
                    }
                }
            }
            
            
            lastMousePosition = Input.mousePosition;
        }


    }
}
