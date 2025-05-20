using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction pauseGame;
    [SerializeField] private GameObject pauseMenu;
    private Animator animator;

    private bool gamePaused = false;

    private void OnEnable()
    {
        var playerControlls = inputSystem.FindActionMap("Player");
        pauseGame = playerControlls.FindAction("Pause");

        pauseGame.Enable();
    }

    private void OnDisable()
    {
        pauseGame.Disable();
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        animator = pauseMenu.GetComponent<Animator>();
    }

    private void Update()
    {
        if (pauseGame.triggered)
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            {
                GamePaused();
            }
            else
            {
                PlayerGameUnpaused();
            }
        }
    }

    private void GamePaused()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    private void PlayerGameUnpaused()
    {
        animator.SetTrigger("Hide");
    }

    public void UnpauseTime()
    {
        // Called when pause menu hide animation finishes
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
