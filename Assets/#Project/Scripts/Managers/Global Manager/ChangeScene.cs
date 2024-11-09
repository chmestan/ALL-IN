using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        scene = scene.Trim();
        Debug.Log("sceneName to load: " + scene);
        SceneManager.LoadScene(scene);
    }

}
