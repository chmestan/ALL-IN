using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public static ChangeScene Instance { get; private set; }

    private void Awake()
    {
        // Singleton design pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void LoadScene(string scene)
    {
        scene = scene.Trim();
        Debug.Log("sceneName to load: " + scene);
        SceneManager.LoadScene(scene);
    }

}
