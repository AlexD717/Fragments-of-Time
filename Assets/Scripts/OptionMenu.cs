using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = FindFirstObjectByType<MusicManager>();
    }

    private void Update()
    {
        float musicVolume = musicVolumeSlider.value;
        musicVolumeText.text = $"Music Volume: {(musicVolume * 100).ToString("F0")}%";
        musicManager.SetMusicVolume(musicVolume);
    }
}