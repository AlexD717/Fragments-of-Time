using UnityEngine;
using UnityEngine.InputSystem;

public class InfoTextManager : MonoBehaviour
{
    private int index = 0;
    private int totalTextsToShow;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction nextText;

    [SerializeField] private bool backgroundInfo;
    [SerializeField] private bool showMenu;
    private bool playerDecision = false;

    private void OnEnable()
    {
        var playerControls = inputActions.FindActionMap("UI");
        nextText = playerControls.FindAction("Next");
        nextText.Enable();

    }

    private void OnDisable()
    {
        nextText.Disable();
    }

    private void Start()
    {
        totalTextsToShow = transform.childCount;
        index = 0;
        ShowText(index);

        Time.timeScale = 0f;
    }

    private void Update()
    {
        inputActions.FindActionMap("Player").Disable();

        if (nextText.triggered && !playerDecision)
        {
            if (index < (totalTextsToShow - 1))
            {
                index++;
                ShowText(index);
            }
            else
            {
                // All text has finished
                if (backgroundInfo)
                {
                    PlayerPrefsManager.BackgroundInfoGiven(); // Makes sure information isn't given twice
                    // Load level select level
                    Loader.LoadByName("LevelSelect");
                }
                else if (showMenu)
                {
                    NextLevelPortal nextLevelPortal = FindFirstObjectByType<NextLevelPortal>();
                    if (nextLevelPortal != null)
                    {
                        nextLevelPortal.ShowMenu();
                    }
                }
                else
                {
                    TextEnded();
                }
            }
        }
    }

    public void PlayerDecided(int _index)
    {
        playerDecision = false;
        index = _index;
        ShowText(_index);
    }

    private void TextEnded()
    {
        Time.timeScale = 1f;
        inputActions.FindActionMap("Player").Enable();
        enabled = false; // Deactivates this component
    }

    private void ShowText(int textIndex)
    {
        if (textIndex == -1)
        {
            TextEnded();
        }

        for (int i = 0; i < totalTextsToShow; i++)
        {
            if (i == textIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                if (transform.GetChild(i).gameObject.CompareTag("Decision"))
                {
                    playerDecision = true;
                }
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
