using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string currentVersion = "0.1";

    private void Start()
    {
        if (PlayerPrefs.GetString("CurrentVersion", "") != currentVersion)
        {
            Debug.Log("Clearing All Player Prefs");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("CurrentVersion", currentVersion);
        }

        Debug.Log("Max Level Past: " + PlayerPrefs.GetInt("MaxLevelPast", 0).ToString());
    }

    public static void LevelPast()
    {
        int maxLevelPassed = GetMaxLevelPast();
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentSceneLevel = int.Parse(currentSceneName.Substring(currentSceneName.Length - 1));

        if (currentSceneLevel > maxLevelPassed)
        {
            PlayerPrefs.SetInt("MaxLevelPast", currentSceneLevel);
            Debug.Log("New Max Level Past: " + currentSceneLevel.ToString());
        }
    }

    public static int GetMaxLevelPast()
    {
        return PlayerPrefs.GetInt("MaxLevelPast", 0);
    }

    public static void SaveFastestLevelPassTime(float passsTime, string passTimeText)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        float fastestPassTime = PlayerPrefs.GetFloat(currentSceneName + "PassTime", Mathf.Infinity);
        if (passsTime <= fastestPassTime)
        {
            PlayerPrefs.SetFloat(currentSceneName + "PassTime", passsTime);
            PlayerPrefs.SetString(currentSceneName + "PassTimeText", passTimeText);
        }
    }

    public static string GetFastestLevelPassTimeText(int level)
    {
        return PlayerPrefs.GetString($"Level{level.ToString()}PassTimeText", "Level Not Passed");
    }

    public static void SetMusicVolume(float musicVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }

    public static void SetSFXVolume(float sfxVolume)
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume", 0.4f);
    }
}