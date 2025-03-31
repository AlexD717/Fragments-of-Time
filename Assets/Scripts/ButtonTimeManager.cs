using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTimeManager : MonoBehaviour
{
    private ButtonPlatform[] buttons;
    private List<bool>[] buttonOn;
    private string sceneName = "";

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void FixedUpdate()
    {
        if (buttons.Length <= 0) { return; }

        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonPlatform button = buttons[i];
            bool buttonPressed = button.buttonPressed;
            buttonOn[i].Add(buttonPressed);
        }
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoadingScreen") { return; }

        if (sceneName != scene.name)
        {
            sceneName = scene.name;
            ClearData();
        }
        else
        {
            if (buttonOn.Length > 0)
            {
                GiveData();
                ClearData();
            }
        }

        buttons = FindObjectsByType<ButtonPlatform>(FindObjectsSortMode.None);
        buttonOn = new List<bool>[buttons.Length];
        // Initialize each list within the array
        for (int i = 0; i < buttonOn.Length; i++)
        {
            buttonOn[i] = new List<bool>();
        }
    }

    private void GiveData()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonPlatform button = buttons[i];
            button.SetTimesOn(new List<bool>(buttonOn[i]));
        }
    }

    private void ClearData()
    {
        buttons = FindObjectsByType<ButtonPlatform>(FindObjectsSortMode.None);
        buttonOn = new List<bool>[buttons.Length];
        // Initialize each list within the array
        for (int i = 0; i < buttonOn.Length; i++)
        {
            buttonOn[i].Clear();
        }
    }
}
