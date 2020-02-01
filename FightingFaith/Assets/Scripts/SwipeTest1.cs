using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest1 : MonoBehaviour
{
    public bool isSwiping;

    Rigidbody2D rb;
    Camera cam;

    Vector2 oldPos;
    Vector2 newPos;

    public float minSlashSpeed;

    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
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
        newPos = cam.ScreenToWorldPoint(Input.mousePosition);

        rb.position = newPos;
        velocity = (newPos - oldPos).magnitude * Time.deltaTime;
        oldPos = newPos;
    }

    void StartSwiping()
    {
        isSwiping = true;
        oldPos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void EndSwiping()
    {
        isSwiping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (velocity > minSlashSpeed)
        {
            Debug.Log("Hit");
        }
    }
}
