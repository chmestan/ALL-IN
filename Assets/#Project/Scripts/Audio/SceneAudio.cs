using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    [Header("Audio"), Space(3f)]
        private AudioManager audioManager;
        [SerializeField] AudioClip sceneMusic;

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
        audioManager.StopMusic();
        audioManager.audioMixer.SetFloat("MusicVolume", Mathf.Log10(0.0001f) * 20); 
        audioManager.audioMixer.SetFloat("MasterVolume", Mathf.Log10(1f) * 20); 
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        yield return new WaitForSeconds(1.0f);

        audioManager.PlayMusic(sceneMusic);
        audioManager.FadeMusicGroup(1.0f, 2.0f); 
    }


}
