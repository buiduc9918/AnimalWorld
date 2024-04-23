using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    TypeSelect,
    TypeMove,
    TypePop,
    TypeGameOver
};

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioClip clip1;
    public static SoundManager Instance;
    public AudioSource Source;

    private void Awake()
    {
        Instance = this;
        Source = GetComponent<AudioSource>();
        Source.clip = clip1;
    }
    private void Start()
    {
        Source.Play();
    }

    public void PlaySound(SoundType clipType)
    {
        Source.PlayOneShot(clips[(int)clipType]);
    }
}