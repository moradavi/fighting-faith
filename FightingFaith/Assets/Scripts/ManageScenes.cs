using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public int sceneNum;
    public bool tapChange;

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
        SceneManager.LoadScene(sceneNum);
    }
}
