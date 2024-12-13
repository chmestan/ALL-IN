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

    public void FadeMusicGroup(float targetVolume, float duration)
    {
        if (activeFadeCoroutine != null)
        {
            StopCoroutine(activeFadeCoroutine);
        }

        activeFadeCoroutine = StartCoroutine(FadeAudioGroupCoroutine("MusicVolume", targetVolume, duration));
    }
    public void FadeMasterVolume(float targetVolume, float duration)
    {
        if (activeFadeCoroutine != null)
        {
            StopCoroutine(activeFadeCoroutine);
        }

        activeFadeCoroutine = StartCoroutine(FadeAudioGroupCoroutine("MasterVolume", targetVolume, duration));
    }

    private IEnumerator FadeAudioGroupCoroutine(string parameterName, float targetVolume, float duration)
    {
        float currentTime = 0;
        audioMixer.GetFloat(parameterName, out float currentVolume);
        currentVolume = Mathf.Pow(10, currentVolume / 20); // Convert from dB to linear scale

        float startVolume = currentVolume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            audioMixer.SetFloat(parameterName, Mathf.Log10(newVolume) * 20);
            yield return null;
        }

        audioMixer.SetFloat(parameterName, Mathf.Log10(targetVolume) * 20);

        activeFadeCoroutine = null; // Reset the coroutine reference
    }}
