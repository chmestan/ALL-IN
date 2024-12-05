using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private bool debug = false;

    // Loads a scene immediately
    public void LoadScene(string scene)
    {
        scene = scene.Trim();
        if (debug) Debug.Log("(ChangeScene) Scene name to load: " + scene);
        SceneManager.LoadScene(scene);
    }
}

