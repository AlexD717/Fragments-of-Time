using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerTimeManager : MonoBehaviour
{
    [SerializeField] private GameObject timeTraveledPlayer;
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction timeTravelRestart;

    private bool actualTimeManager = false;
    private List<Vector3> realPlayerPositions;
    private Transform realPlayer;

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
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        realPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        if (realPlayerPositions.Count > 0)
        {
            InstantiateTimeTraveledPlayer();
            realPlayerPositions.Clear();
        }
    }

    private void Update()
    {
        if (timeTravelRestart.triggered)
        {
            TimeTravelRestart();
        }
    }

    private void FixedUpdate()
    {
        RecordPlayerPosition();
    }

    private void RecordPlayerPosition()
    {
        realPlayerPositions.Add(realPlayer.position);
    }

    private void InstantiateTimeTraveledPlayer()
    {
        GameObject spawnedPlayer = Instantiate(timeTraveledPlayer);
        spawnedPlayer.GetComponent<PlayerTimeTraveled>().positions = new List<Vector3>(realPlayerPositions);
    }

    private void TimeTravelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restarts current scene
    }
}
