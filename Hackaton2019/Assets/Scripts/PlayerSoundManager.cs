using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public static PlayerSoundManager instance = null;
    AudioSource audioSource;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
