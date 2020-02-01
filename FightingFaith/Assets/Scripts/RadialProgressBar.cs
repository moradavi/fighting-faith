using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    public Transform loadingBar;
    public float currentAmount;
    public float speed;

    public ManageScenes sceneManager;

    void Update()
    {
    
        if (Input.GetMouseButton(0) && currentAmount < 100)
        {
            currentAmount += speed * Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentAmount = 0;
        }

        if (currentAmount >= 100)
        {
            sceneManager.ChangeScene();
        }

        loadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
    }
}
