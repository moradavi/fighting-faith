using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{

    public GameObject[] defensePoints;
    public int numOfPoints;

    public float timeLimit;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeLimit)
        {
            Debug.Log("GAME OVER");
        }

    }
}
