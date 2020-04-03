using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlash : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void StopRedFlash()
    {
        GetComponent<Animator>().enabled = false;
    }
}
