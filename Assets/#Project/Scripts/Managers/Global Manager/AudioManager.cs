using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private Coroutine activeFadeCoroutine;

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

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void FadeMasterVolume(float targetVolume, float duration)
    {
        if (activeFadeCoroutine != null)
        {
            StopCoroutine(activeFadeCoroutine);
        }

        activeFadeCoroutine = StartCoroutine(FadeMasterVolumeCoroutine(targetVolume, duration));
    }

    private IEnumerator FadeMasterVolumeCoroutine(float targetVolume, float duration)
    {
        float currentTime = 0;
        audioMixer.GetFloat("MasterVolume", out float currentVolume);
        currentVolume = Mathf.Pow(10, currentVolume / 20); 

        float startVolume = currentVolume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(newVolume) * 20);
            yield return null;
        }

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(targetVolume) * 20);

        if (Mathf.Approximately(targetVolume, 0.0001f))
        {
            StopMusic();
        }

        activeFadeCoroutine = null; 
    }
}
