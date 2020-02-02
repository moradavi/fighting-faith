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
        if (tapChange && Input.GetMouseButtonDown(0))
        {
            LoadNewScene();
        }
    }

    public void ChangeScene()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(sceneNum);
    }
}
