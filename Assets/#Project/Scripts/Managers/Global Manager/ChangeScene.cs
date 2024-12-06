using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private bool debug = true;
    private Animator transitionAnim;

    private void Awake()
    {
        transitionAnim = GetComponentInChildren<Animator>();
        if (transitionAnim == null) Debug.LogError("(ChangeScene) Animator not found");
    }

   public void LoadSceneWithTransition(string scene, float delay)
    {
        if (transitionAnim != null)
        {
            StartCoroutine(SceneTransition(scene.Trim(), delay));
        }
        // else
        // {
        //     // fallback
        //     if (debug) Debug.LogWarning("(ChangeScene) Transition Animator not found, loading scene without transition.");
        //     LoadSceneWithDelay(scene, delay);
        // }
    }

    private IEnumerator SceneTransition(string scene, float delay)
    {
        if (debug) Debug.Log("(ChangeScene) Starting scene transition.");

        transitionAnim.SetTrigger("SlideLeft");
        yield return new WaitForSeconds(delay);

        if (debug) Debug.Log($"(ChangeScene) Loading scene: {scene}");
        SceneManager.LoadScene(scene);

        yield return new WaitForSeconds(0.1f); 

        transitionAnim.SetTrigger("SlideRight");
    }


    #region fallback
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
    #endregion
}

