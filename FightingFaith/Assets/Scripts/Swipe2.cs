using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe2 : MonoBehaviour
{
    Vector3 lastMousePosition;
    public float swipeSpeedThreshold;

    public GameObject mouseSprite;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;
        
        if (Input.GetMouseButton(0))
        {
            mouseSprite.gameObject.transform.position = mousePos;

            Collider2D[] frameCol = Physics2D.OverlapPointAll(new Vector2(mousePos.x, mousePos.y), LayerMask.GetMask("Enemy"));
            if ((Input.mousePosition - lastMousePosition).sqrMagnitude > swipeSpeedThreshold)
            {
                foreach(Collider2D col in frameCol)
                {
                    Debug.Log("hit");
                }
            }
            
            lastMousePosition = Input.mousePosition;
        }


    }
}
