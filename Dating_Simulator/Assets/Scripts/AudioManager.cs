using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private static AudioManager instance;
    private AudioClip clipToPlay;

    public static AudioManager Instance { get { return instance; } }
    private void Awake()
    {    
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip soundToPlay)
    {       
        audioSource.clip = soundToPlay;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
    }
}
