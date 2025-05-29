using UnityEngine;
using UnityEngine.InputSystem;

public class InfoTextManager : MonoBehaviour
{
    private int index = 0;
    private int totalTextsToShow;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction nextText;

    [SerializeField] private bool backgroundInfo;

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
    }

    private void Update()
    {
        if (nextText.triggered)
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
                else
                {
                    NextLevelPortal nextLevelPortal = FindFirstObjectByType<NextLevelPortal>();
                    if (nextLevelPortal != null)
                    {
                        nextLevelPortal.ShowMenu();
                    }
                }
            }
        }
    }

    private void ShowText(int textIndex)
    {
        for (int i = 0; i < totalTextsToShow; i++)
        {
            if (i == textIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
