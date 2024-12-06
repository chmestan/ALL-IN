using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private bool debug = true;

    public void LoadSceneWithDelay(string scene, float delay)
    {
        StartCoroutine(DelayedSceneLoad(scene.Trim(), delay));
    }

    private IEnumerator DelayedSceneLoad(string scene, float delay)
    {
        if (debug) Debug.Log($"(ChangeScene) Delaying scene load for {delay} seconds.");
        yield return new WaitForSeconds(delay);

        if (debug) Debug.Log($"(ChangeScene) Loading scene: {scene}");
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene)
    {
        LoadSceneWithDelay(scene, 0); 
    }
}

