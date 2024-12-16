using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButton : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private AudioClip cantClickAudioClip;

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }

    public void OnClick()
    {
        audioManager.PlaySFX(cantClickAudioClip);
    }

}
