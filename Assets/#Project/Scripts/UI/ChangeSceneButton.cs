using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    private ChangeScene changeScene;
    private Button button;
    private AudioManager audioManager;
    [SerializeField] private AudioClip clickAudioClip;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        changeScene = GlobalManager.Instance.GetComponent<ChangeScene>();
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }

    public void OnClick(string sceneName)
    {
        DisableAllButtons();
        audioManager.PlaySFX(clickAudioClip, 20f);
        changeScene.LoadSceneWithTransition(sceneName, 1f);
    }

    private void DisableAllButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            button.interactable = false;
        }
    }

}
