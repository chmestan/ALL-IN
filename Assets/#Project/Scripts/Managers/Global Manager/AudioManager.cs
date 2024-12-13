using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Play a specific SFX
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Play or change the background music
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    // public void SetMusicVolume(float volume)
    // {
    //     audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); 
    // }

    // public void SetSFXVolume(float volume)
    // {
    //     audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    // }
}
