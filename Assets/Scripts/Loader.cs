using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
   public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
