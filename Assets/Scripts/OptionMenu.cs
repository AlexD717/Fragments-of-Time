using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;
    private SoundManager soundManager;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        soundManager = FindFirstObjectByType<SoundManager>();
        
        musicVolumeSlider.value = soundManager.GetMusicVolume();
        sfxVolumeSlider.value = soundManager.GetSFXVolume();
    }

    private void Update()
    {
        float musicVolume = musicVolumeSlider.value;
        musicVolumeText.text = $"Music Volume: {(musicVolume * 100).ToString("F0")}%";
        soundManager.SetMusicVolume(musicVolume);

        float sfxVolume = sfxVolumeSlider.value;
        sfxVolumeText.text = $"SFX Volume: {(sfxVolume * 100).ToString("F0")}%";
        soundManager.SetSFXVolume(sfxVolume);
    }

    public void CloseOptionMenu()
    {
        animator.SetTrigger("Hide");
    }

    public void HideOptionMenu()
    {
        gameObject.SetActive(false);
    }
}