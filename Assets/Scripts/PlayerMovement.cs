using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction horizontalMovement;
    private float horizontalMovementValue;

    private void OnEnable()
    {
        

        horizontalMovement.Enable();
    }

    private void OnDisable()
    {
        horizontalMovement.Disable();
    }

    private void Start()
    {
        // Find input actions
        var playerControlls = inputSystem.FindActionMap("Player");
        horizontalMovement = playerControlls.FindAction("HorizontalMovement");
    }

    private void Update()
    {
        // Get input value
        horizontalMovementValue = horizontalMovement.ReadValue<float>();

        Debug.Log(horizontalMovementValue);
    }
}
