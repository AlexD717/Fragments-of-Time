using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string currentVersion = "0.0";

    private bool trueManager;

    private void Awake()
    {
        // Makes sure their is only one PlayerPrefsManager and makes it so that its not destroyed on load
        // Not needed if all functions are public static
        /*
        if (FindObjectsByType<PlayerPrefsManager>(FindObjectsSortMode.None).Length > 1 && !trueManager)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            trueManager = true;
        }*/
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("CurrentVersion", "") != currentVersion)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("CurrentVersion", currentVersion);
        }

        Debug.Log("Max Level Past: " + PlayerPrefs.GetInt("MaxLevelPast", 0).ToString());
    }

    public static void LevelPast()
    {
        int maxLevelPassed = PlayerPrefs.GetInt("MaxLevelPast", 0);
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);
        int currentSceneLevel = int.Parse(currentSceneName.Substring(currentSceneName.Length - 1));
        Debug.Log(currentSceneLevel);
        if (currentSceneLevel > maxLevelPassed)
        {
            PlayerPrefs.SetInt("MaxLevelPast", currentSceneLevel);
            Debug.Log("New Max Level Past: " + currentSceneLevel.ToString());
        }
    }
}
