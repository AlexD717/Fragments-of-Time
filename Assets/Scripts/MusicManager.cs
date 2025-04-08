using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgMusic;
    private List<AudioClip> audioResources;

    private AudioSource audioSource;

    private bool realAudioManager = false;  

    private void Awake()
    {
        if (FindObjectsByType<MusicManager>(FindObjectsSortMode.None).Length > 1 && !realAudioManager)
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
        audioSource = GetComponent<AudioSource>();

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
}