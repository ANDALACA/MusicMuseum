using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInstrument : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip noteToPlay)
    {
        audioSource.PlayOneShot(noteToPlay);
    }
}
