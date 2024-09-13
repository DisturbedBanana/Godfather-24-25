using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public string sceneName;
    private SceneManager sceneManager;

    private void Start()
    {
        sceneManager = new SceneManager();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManager.LoadScene(sceneName);
        }
    }
}
