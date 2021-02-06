using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource music = null;
    [SerializeField] private AudioClip intro = null;
    [SerializeField] private AudioClip loop = null;
    [SerializeField] private AudioSource[] transitions = null;

    private void Awake()
    {
        AudioController.Instance.ChangeMusicTrack(music);
    }

    private void Update()
    {
        if (!music.isPlaying)
        {
            music.PlayOneShot(loop);
            music.loop = true;
        }
    }
}
