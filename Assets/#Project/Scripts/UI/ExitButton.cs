using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
private AudioManager audioManager;
    [SerializeField] private AudioClip clickedAudioClip;

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }

    public void OnClick()
    {
        audioManager.PlaySFX(clickedAudioClip);
        ExitGame();
    }

    private void ExitGame()
    {
        Debug.Log("(ExitButton) Game is exiting");
        Application.Quit();
    }
}
