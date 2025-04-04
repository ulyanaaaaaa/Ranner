using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource coinSound;

    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void PlayCoinSound()
    {
        coinSound.Play();
    }
}