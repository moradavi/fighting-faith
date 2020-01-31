using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public int sceneNum;

    void Update()
    {
        ChangeScene();
    }

    void ChangeScene()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(sceneNum);
        }
    }
}
