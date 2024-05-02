using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource SFXSource;

    [Header("---------- Audio Clips ----------")]
    public AudioClip buttonClick;

    public AudioClip success;
    public AudioClip fail;

    // This static variable will hold the instance of AudioManager
    public static AudioManager instance;

    private void Awake()
    {
        // If instance is not assigned yet
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This makes the object persistent across scenes
        }
        else
        {
            // If there is already an instance of AudioManager and it's not this one, destroy this one
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}