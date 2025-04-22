using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXObject : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SoundManager soundManager = FindFirstObjectByType<SoundManager>();
        if (soundManager != null)
        {
            audioSource.volume = soundManager.GetSFXVolume();
        }
    }
}