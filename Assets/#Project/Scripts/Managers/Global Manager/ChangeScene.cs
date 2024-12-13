using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private Animator transitionAnim;
    private AudioManager audioManager;
    private bool isSceneChanging = false; 
    [SerializeField] private bool debug = true;

    private void Awake()
    {
        transitionAnim = GetComponentInChildren<Animator>();
        if (transitionAnim == null) Debug.LogError("(ChangeScene) Animator not found");
    }

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
        transitionAnim.SetTrigger("SlideRight");
    }

    public void LoadSceneWithFixedTransition(string scene)
    {
        if (isSceneChanging) return;
        isSceneChanging = true;

        if (transitionAnim != null)
        {
            StartCoroutine(SceneTransition(scene.Trim(), 1f));
        }
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

        if (audioManager != null)
        {
            audioManager.FadeMasterVolume(0.0001f, delay);
        }

        transitionAnim.SetTrigger("SlideLeft");

        yield return new WaitForSeconds(delay);

        if (debug) Debug.Log($"(ChangeScene) Loading scene: {scene}");
        SceneManager.LoadScene(scene);

        yield return new WaitForSeconds(0.1f);
        isSceneChanging = false;
        transitionAnim.SetTrigger("SlideRight");
    }
}

