using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRenderer : MonoBehaviour
{
    Vector3 lastMousePosition;
    public float swipeSpeedThreshold;
    private ParticleSystem ps;

    public GameObject particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Debug.Log("Play");
        //    ps.enableEmission = true;
        //} else
        //{
        //    ps.enableEmission = false;
        //}

        if (Input.GetMouseButton(0))
        {
            particleSystem.SetActive(true);
        } else
        {
            particleSystem.SetActive(false);
        }
    }
}
