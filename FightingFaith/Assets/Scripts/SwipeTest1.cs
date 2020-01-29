using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest1 : MonoBehaviour
{
    public bool isSwiping;

    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D circleColl;

    Vector2 oldPos;
    public float minSlashSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        circleColl = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSwiping();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSwiping();
        }

        if (isSwiping)
        {
            UpdateSwipe();
        }
    }

    void UpdateSwipe()
    {
        Vector2 newPos = cam.ScreenToWorldPoint(Input.mousePosition);

        rb.position = newPos;
        float velocity = (newPos - oldPos).magnitude * Time.deltaTime;

        if (velocity > minSlashSpeed)
        {
            circleColl.enabled = true;
        } else
        {
            circleColl.enabled = false;
        }

        oldPos = newPos;
    }

    void StartSwiping()
    {
        isSwiping = true;
        oldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        circleColl.enabled = false;
    }
    void EndSwiping()
    {
        isSwiping = false;
        circleColl.enabled = false;
    }
}
