using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SceneTimer : MonoBehaviour
{
    //Create Variable timer
    public float timer;

    void Start()
    {
        //Reset timer
        timer = 0;
    }

    void Update()
    {
        //Run timer
        timer += Time.deltaTime;
    }
}
