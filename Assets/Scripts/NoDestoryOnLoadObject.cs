using UnityEngine;
using UnityEngine.SceneManagement;

public class NoDestoryOnLoadObject : MonoBehaviour
{
    [SerializeField] private bool restartOnDifferentSceneLoad;
    private string startedScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        startedScene = SceneManager.GetActiveScene().name;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (restartOnDifferentSceneLoad)
        {
            if (startedScene != scene.name)
            {
                Destroy(gameObject);
            }
        }
    }
}
