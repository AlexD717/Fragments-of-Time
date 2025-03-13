using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerTimeManager : MonoBehaviour
{
    [SerializeField] private GameObject timeTraveledPlayer;
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction timeTravelRestart;

    private GroundCheck groundCheck;

    private bool actualTimeManager = false;
    private List<Vector3> realPlayerPositions;
    private List<float> realPlayerXScale;
    private Transform realPlayer;
    private List<float> landedOnGroundTimes;
    private List<bool> isGroundedList;

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
        landedOnGroundTimes = new List<float>();
        isGroundedList = new List<bool>();
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        realPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        if (realPlayerPositions.Count > 0)
        {
            InstantiateTimeTraveledPlayer();
            ClearPlayerData();
        }
        groundCheck = FindFirstObjectByType<GroundCheck>();
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
        RecordPlayerData();
    }

    private void RecordPlayerData()
    {
        realPlayerPositions.Add(realPlayer.position);
        realPlayerXScale.Add(realPlayer.transform.localScale.x);

        isGroundedList.Add(groundCheck.isGrounded);
    }

    public void PlayerLandedOnGround(float time)
    {
        landedOnGroundTimes.Add(time);
    }

    private void ClearPlayerData()
    {
        realPlayerPositions.Clear();
        realPlayerXScale.Clear();
        landedOnGroundTimes.Clear();
        isGroundedList.Clear();
    }

    private void InstantiateTimeTraveledPlayer()
    {
        GameObject spawnedPlayer = Instantiate(timeTraveledPlayer);
        PlayerTimeTraveled playerTimeTraveled = spawnedPlayer.GetComponent<PlayerTimeTraveled>();
        playerTimeTraveled.positions = new List<Vector3>(realPlayerPositions);
        playerTimeTraveled.xScale = new List<float>(realPlayerXScale);
        playerTimeTraveled.landedOnGroundTimes = new List<float>(landedOnGroundTimes);
        playerTimeTraveled.isGroundedList = new List<bool>(isGroundedList);
    }

    private void TimeTravelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restarts current scene
    }
}
