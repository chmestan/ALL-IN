using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private bool debug = true;
    private Animator transitionAnim;
    private bool isSceneChanging = false; 

    private void Awake()
    {
        transitionAnim = GetComponentInChildren<Animator>();
        if (transitionAnim == null) Debug.LogError("(ChangeScene) Animator not found");
    }

    private void Start()
    {
        transitionAnim.SetTrigger("SlideRight");
    }

    public void LoadSceneWithTransition(string scene, float delay)
    {
        if (isSceneChanging) return;
        isSceneChanging = true;

        if (transitionAnim != null)
        {
            StartCoroutine(SceneTransition(scene.Trim(), delay));
        }
    }

    private IEnumerator SceneTransition(string scene, float delay)
    {
        if (debug) Debug.Log("(ChangeScene) Starting scene transition.");

        transitionAnim.SetTrigger("SlideLeft");
        yield return new WaitForSeconds(delay);

        if (debug) Debug.Log($"(ChangeScene) Loading scene: {scene}");
        SceneManager.LoadScene(scene);

        yield return new WaitForSeconds(0.1f);

        isSceneChanging = false; 
        transitionAnim.SetTrigger("SlideRight");
    }
}

