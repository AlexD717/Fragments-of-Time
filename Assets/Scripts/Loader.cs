using System;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static Action onLoaderCallback;

   public static void LoadByName(string scene)
    {
        SceneManager.LoadScene("LoadingScreen");

        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene);
        };
    }

    public static void LoadByIndex(int buildIndex)
    {
        SceneManager.LoadScene("LoadingScreen");

        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(buildIndex);
        };
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}