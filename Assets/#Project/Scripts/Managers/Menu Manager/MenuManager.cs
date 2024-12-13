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
        StartCoroutine(PlayMenuMusicWithDelay()); 
    }

    private IEnumerator PlayMenuMusicWithDelay()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(audioManager.FadeInMusic(menuMusic, 2.0f)); 
    }

}
