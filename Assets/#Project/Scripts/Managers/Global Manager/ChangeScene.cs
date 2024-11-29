using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] private bool debug = false;

    public void LoadScene(string scene)
    {
        scene = scene.Trim();
        if (debug) Debug.Log("(ChangeScene) sceneName to load: " + scene);
        SceneManager.LoadScene(scene);
    }

}
