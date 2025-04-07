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
    }
}
