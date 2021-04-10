using UnityEngine;
using System;

public class MusicPlayer : MonoBehaviour
{
    public static event Action<MusicPlayer> OnAnyMusicEnter;

    [SerializeField] private AudioClip intro = null;
    [SerializeField] private AudioClip loop = null;
    [SerializeField] private AudioSource[] transitions = null;

    private void Awake()
    {
        OnAnyMusicEnter?.Invoke(this);
    }

    private void OnEnable()
    {
        AudioController.Instance.ChangeTrack(intro);
        AudioController.Instance.SetLoop(false);
        AudioController.Instance.Play();
    }

    private void Update()
    {
        if (!AudioController.Instance.IsPlaying())
        {
            AudioController.Instance.ChangeTrack(loop);
            AudioController.Instance.SetLoop(true);
            AudioController.Instance.Play();
        }
    }
}
