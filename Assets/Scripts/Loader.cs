using System;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static Action onLoaderCallback;

   public static void Load(string scene)
    {
        SceneManager.LoadScene("LoadingScreen");

        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene);
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
