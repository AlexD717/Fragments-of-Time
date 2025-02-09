using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTimeManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction timeTravelRestart;

    private bool actualTimeManager = false;

    private void OnEnable()
    {
        // Find input actions
        var playerControlls = inputSystem.FindActionMap("Player");
        timeTravelRestart = playerControlls.FindAction("TimeTravelRestart");

        timeTravelRestart.Enable();
    }

    private void OnDisable()
    {
        timeTravelRestart.Disable();
    }

    private void Awake()
    {
        // Makes sure that there can only be one PlayerTimeManager
        if (GameObject.FindGameObjectsWithTag("PlayerTimeManager").Length > 1 && !actualTimeManager) 
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            actualTimeManager = true;
        }
    }

    private void Update()
    {
        if (timeTravelRestart.triggered)
        {
            TimeTravelRestart();
        }
    }

    private void TimeTravelRestart()
    {
        
    }
}
