using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }


    public IEnumerator FadeMasterVolume(float targetVolume, float duration)
    {
        float currentTime = 0;

        audioMixer.GetFloat("MasterVolume", out float currentVolume);
        currentVolume = Mathf.Pow(10, currentVolume / 20); // Convert from dB to linear 

        float startVolume = currentVolume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(newVolume) * 20); // back to decibels
            yield return null;
        }

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(targetVolume) * 20); 
    }

}
