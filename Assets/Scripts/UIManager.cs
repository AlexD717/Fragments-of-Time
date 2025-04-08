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

    public void LoadScreenShatterLevel(string levelNum)
    {
        ScreenShatterAnimation screenShatterAnimation = FindFirstObjectByType<ScreenShatterAnimation>();
        if (screenShatterAnimation != null)
        {
            screenShatterAnimation.ShatterScreen("Level" + levelNum);
        }
        else
        {
            Loader.LoadByName("Level" + levelNum);
        }
    }
}