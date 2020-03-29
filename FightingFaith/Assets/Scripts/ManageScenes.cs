using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class ManageScenes : MonoBehaviour
{
    public int sceneNum;
    public bool tapChange;
    public int currentScene;
    public Health playerHealth;
    public GameObject timerTracker;


    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void Update()
    {
        //Load new scene if tapChange boolean is checked and mouse is clicked
        if (tapChange && Input.GetMouseButtonDown(0))
        {
            LoadNewScene();
        }
    }

    //Enable animator if the component is not null
    public void ChangeScene()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    //Load new sccene
    public void LoadNewScene()
    {
        //Check if in the encounter scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playerHealth != null)
            {
                Analytics.CustomEvent("EndOfEncounter", new Dictionary<string, object>
                {
                { "Player Health", playerHealth.CurrentHealth }
                });
            }
        }

        //Analytics.CustomEvent("SceneTimer");


        timerTracker.GetComponent<AnalyticsEventTracker>().TriggerEvent();

        SceneManager.LoadScene(sceneNum);       
    }
}
