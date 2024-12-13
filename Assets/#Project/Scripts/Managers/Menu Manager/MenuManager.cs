using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Audio"), Space(3f)]
        private AudioManager audioManager;
        [SerializeField] AudioClip menuMusic;

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
        StartCoroutine(FadeInMasterVolume());
    }

    private IEnumerator FadeInMasterVolume()
    {
        audioManager.audioMixer.SetFloat("MasterVolume", Mathf.Log10(0.0001f) * 20); 
        yield return new WaitForSeconds(1.0f);

        audioManager.PlayMusic(menuMusic);
        audioManager.FadeMasterVolume(1.0f, 2.0f); 
    }


}
