using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerTimeManager : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private bool screenShatterOnTimeWarp;

    [Header("References")]
    [SerializeField] private GameObject timeTraveledPlayer;
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction timeTravelRestart;

    private bool actualTimeManager = false;
    private List<Vector3> realPlayerPositions;
    private List<float> realPlayerXScale;
    private Transform realPlayer;
    private PlayerMovement realPlayerMovement;
    private List<int> animationStates;

    private string sceneName;

    public int timesRestarted = 0;
    public float totalTimeTaken = 0f;

    private void OnEnable()
    {
        // Find input actions
        var playerControlls = inputSystem.FindActionMap("Player");
        timeTravelRestart = playerControlls.FindAction("TimeTravelRestart");

        timeTravelRestart.Enable();

        // Calls OnSceneLoad when scene loaded
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        if (timeTravelRestart != null)
        {
            timeTravelRestart.Disable();
        }

        // Unsubscribes from scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void Awake()
    {
        // Makes sure that there can only be one PlayerTimeManager
        if (GameObject.FindGameObjectsWithTag("PlayerTimeManager").Length > 1 && !actualTimeManager) 
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            actualTimeManager = true;
        }

        realPlayerPositions = new List<Vector3>();
        realPlayerXScale = new List<float>();
        animationStates = new List<int>();

        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameObject realPlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (realPlayerObject == null)
        {
            ClearPlayerData();
            return;
        }

        realPlayer = realPlayerObject.transform;
        realPlayerMovement = realPlayer.GetComponent<PlayerMovement>();

        if (sceneName != scene.name)
        {
            sceneName = scene.name;
            timesRestarted = 0;
            totalTimeTaken = 0;
            ClearPlayerData();
        }
        else
        {
            if (realPlayerPositions.Count > 0)
            {
                InstantiateTimeTraveledPlayer();
                ClearPlayerData();
            }
        }
    }

    private void Update()
    {
        totalTimeTaken += Time.deltaTime;

        if (timeTravelRestart.triggered)
        {
            TimeTravelRestart();
        }
    }

    private void FixedUpdate()
    {
        RecordPlayerData();
    }

    private void RecordPlayerData()
    {
        if (realPlayer == null) { return; }

        realPlayerPositions.Add(realPlayer.position);
        realPlayerXScale.Add(realPlayer.transform.localScale.x);

        animationStates.Add(realPlayerMovement.GetAnimationState());
    }

    private void ClearPlayerData()
    {
        realPlayerPositions.Clear();
        realPlayerXScale.Clear();
        animationStates.Clear();
    }

    private void InstantiateTimeTraveledPlayer()
    {
        GameObject spawnedPlayer = Instantiate(timeTraveledPlayer);
        PlayerTimeTraveled playerTimeTraveled = spawnedPlayer.GetComponent<PlayerTimeTraveled>();
        playerTimeTraveled.positions = new List<Vector3>(realPlayerPositions);
        playerTimeTraveled.xScale = new List<float>(realPlayerXScale);
        playerTimeTraveled.animationStates = new List<int>(animationStates);
    }

    public void TimeTravelRestart()
    {
        timesRestarted++;

        // Spawns a screen shatter animation if needed
        if (screenShatterOnTimeWarp)
        {
            SingleScreenShatterSpawner singleScreenShatterSpawner = FindFirstObjectByType<SingleScreenShatterSpawner>();
            if (singleScreenShatterSpawner != null)
            {
                singleScreenShatterSpawner.SpawnSingleScreenShatter();
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restarts current scene
    }
}
