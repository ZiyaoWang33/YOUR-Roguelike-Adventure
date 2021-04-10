using UnityEngine;
using UnityEngine.Audio;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioSource source = null;
    [SerializeField] private AudioMixer mixer = null;

    private MusicPlayer music = null;

    private void OnEnable()
    {
        MusicPlayer.OnAnyMusicEnter += OnAnyMusicEnterEventHandler;
    }

    private void OnAnyMusicEnterEventHandler(MusicPlayer music)
    {
        this.music = music;
    }

    public void ToggleLevelMusic()
    {
        music.enabled = !music.enabled;
    }

    public void ChangeMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
    }

    public void ChangeTrack(AudioClip track)
    {
        source.clip = track;
    }

    public void Play()
    {
        source.Stop();
        source.PlayOneShot(source.clip);
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }

    public void SetLoop(bool loop)
    {
        source.loop = loop;
    }
}
