using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audios;
    [SerializeField] private AudioSource audioSource;
    public void PlayRandomSound()
    {
        var randSound = Random.Range(0, audios.Count);

        audioSource.clip = audios[randSound];
        audioSource.Play();
    }
}
