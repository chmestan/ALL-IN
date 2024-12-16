using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private AudioClip introClip; 
    [SerializeField] private AudioClip loopClip;  
    private AudioSource musicSource;

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
        musicSource = audioManager.musicSource; 
        audioManager.audioMixer.SetFloat("MasterVolume", Mathf.Log10(1f) * 20); 
        audioManager.audioMixer.SetFloat("MusicVolume", Mathf.Log10(1f) * 20); 
        audioManager.audioMixer.SetFloat("SFXVolume", Mathf.Log10(1f) * 20); 
        StartCoroutine(PlayMenuMusic());
    }

    private IEnumerator PlayMenuMusic()
    {
        yield return new WaitForSeconds(1f);

        musicSource.clip = introClip;
        musicSource.loop = false; 
        musicSource.Play();

        yield return new WaitForSeconds(introClip.length);

        musicSource.clip = loopClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
