using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EM_AUDIO_TYPE
{
    REMOVE,
    LAND,
}

public class AudioManager : MonoBehaviour
{
    private static AudioSource _audioSource;
    
    public AudioClip removeCLip;
    
    public AudioClip landClip;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(EM_AUDIO_TYPE type)
    {
        switch (type)
        {
            case EM_AUDIO_TYPE.REMOVE:
                _audioSource.PlayOneShot(removeCLip);
                break;
            case EM_AUDIO_TYPE.LAND:
                _audioSource.PlayOneShot(landClip);
                break;
        }
    }
}
