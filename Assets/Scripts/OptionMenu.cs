using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    private MusicManager musicManager;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        musicManager = FindFirstObjectByType<MusicManager>();
        
        gameObject.SetActive(false);
    }

    private void Update()
    {
        float musicVolume = musicVolumeSlider.value;
        musicVolumeText.text = $"Music Volume: {(musicVolume * 100).ToString("F0")}%";
        musicManager.SetMusicVolume(musicVolume);
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