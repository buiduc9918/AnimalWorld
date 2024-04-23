using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip yourSoundClip;

    void Start()
    {
        // Gán âm thanh cho AudioSource
        audioSource.clip = yourSoundClip;

        audioSource.Play();
    }

    private void Update()
    {
    }
}
