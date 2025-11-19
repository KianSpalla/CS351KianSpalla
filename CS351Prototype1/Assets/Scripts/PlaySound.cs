using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    private AudioSource audioSourcce;
    public AudioClip soundToPlay;
    public float volume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSourcce = GetComponent<AudioSource>();

        audioSourcce.PlayOneShot(soundToPlay, volume);
    }
}
