using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadLevel(string levelNum)
    {
        Loader.LoadByName("Level" + levelNum);
    }

    public void LoadByName(string sceneName)
    {
        Loader.LoadByName(sceneName);
    }

    public void LoadByIndex(int sceneIndex)
    {
        Loader.LoadByIndex(sceneIndex);
    }

    public void NextLevel()
    {
        Loader.LoadByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenuPlayButton()
    {
        string levelName = "LevelSelect";
        if (!PlayerPrefsManager.GetBackgroundInfoGiven())
        {
            levelName = "BackgroundInfo";
        }
        ScreenShatterAnimation screenShatterAnimation = FindFirstObjectByType<ScreenShatterAnimation>();
        if (screenShatterAnimation != null)
        {
            screenShatterAnimation.ShatterScreen(levelName);
        }
        else
        {
            Loader.LoadByName(levelName);
        }
    }

    public void ShowObject(GameObject objectToShow)
    {
        objectToShow.SetActive(true);
    }
}