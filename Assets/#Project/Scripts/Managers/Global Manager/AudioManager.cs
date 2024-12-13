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

    public IEnumerator FadeInMusic(AudioClip clip, float duration)
    {
        musicSource.clip = clip;
        musicSource.volume = 0;
        musicSource.Play();

        float targetVolume = 1.0f; 
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, targetVolume, currentTime / duration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    }

}
