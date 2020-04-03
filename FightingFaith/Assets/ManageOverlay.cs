using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageOverlay : MonoBehaviour
{
    public GameObject player;
    public GameObject[] overlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Health>().CurrentHealth == 2)
        {
            overlay[0].SetActive(true);
            overlay[1].SetActive(false);
        } else if (player.GetComponent<Health>().CurrentHealth == 1)
        {
            overlay[0].SetActive(false);
            overlay[1].SetActive(true);
        }
    }
}
