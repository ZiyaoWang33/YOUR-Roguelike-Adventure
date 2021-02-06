using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioSource music = null;
    [SerializeField] private AudioSource sfx = null;

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        music.volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        sfx.volume = volume;
    }

    public void ChangeMusicTrack(AudioSource track)
    {
        music = track;
    }

    public void ChangeSFXTrack(AudioSource track)
    {
        sfx = track;
    }
}
