using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject passMenu;
    [SerializeField] private InputActionAsset inputSystem;
    private InputActionMap playerControls;

    private void OnEnable()
    {
        playerControls = inputSystem.FindActionMap("Player");
    }

    private void OnDisable()
    {
        playerControls = inputSystem.FindActionMap("Player");
    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        playerControls.Enable();

        passMenu.SetActive(false);
    }

    public void LevelPast()
    {
        Debug.Log("Player Past Level");
        Time.timeScale = 0f;
        playerControls.Disable();
    }

    public void ShowWinMenu()
    {
        Debug.Log("Showing Win Menu");
        passMenu.SetActive(true);

        PlayerTimeManager playerTimeManager = FindFirstObjectByType<PlayerTimeManager>();
        int timesRestarted = playerTimeManager.timesRestarted;
        float totalTimeTaken = playerTimeManager.totalTimeTaken;
        string timeTakenText = "Time Taken: ";
        if (totalTimeTaken >= 60)
        {
            int minutes = (int)Mathf.Floor(totalTimeTaken / 60f);
            float seconds = totalTimeTaken - (minutes * 60);
            timeTakenText += $"{minutes.ToString()} minutes {seconds.ToString("F2")} seconds";
        }
        else
        {
            timeTakenText += totalTimeTaken.ToString("F2") + " seconds";
        }

        passMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Time Restarted: " + timesRestarted;
        passMenu.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = timeTakenText;
    }
}
