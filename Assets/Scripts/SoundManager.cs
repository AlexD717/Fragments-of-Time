using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgMusic;
    private List<AudioClip> audioResources;

    private AudioSource audioSource;
    private float sfxVolume;

    private bool realAudioManager = false;  

    private void Awake()
    {
        if (FindObjectsByType<SoundManager>(FindObjectsSortMode.None).Length > 1 && !realAudioManager)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
            return;
        }
        else
        {
            realAudioManager = true;
            DontDestroyOnLoad(gameObject);
        }   
    }

    private void Start()
    {
        sfxVolume = PlayerPrefsManager.GetSFXVolume();

        audioSource = GetComponent<AudioSource>();
        SetMusicVolume(PlayerPrefsManager.GetMusicVolume());

        audioResources = new List<AudioClip>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            NewSong();
        }
    }

    private void NewSong()
    {
        if (audioResources.Count == 0)
        {
            foreach (AudioClip audioResource in bgMusic)
            {
                audioResources.Add(audioResource);
            }
        }

        audioSource.clip = audioResources[Random.Range(0, audioResources.Count)];
        audioResources.Remove(audioSource.clip);
        audioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefsManager.SetMusicVolume(volume);
    }

    public float GetMusicVolume()
    {
        return audioSource.volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefsManager.SetSFXVolume(volume);
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}