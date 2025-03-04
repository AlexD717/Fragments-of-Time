using System.Runtime.CompilerServices;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void LoadLevel(string levelNum)
    {
        Loader.Load("Level" + levelNum);
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
            Loader.Load("Level" + levelNum);
        }
    }
}