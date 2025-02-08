using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float speed;

    [Header("References")]
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction horizontalMovement;
    private float horizontalMovementValue;

    private void OnEnable()
    {
        // Find input actions
        var playerControlls = inputSystem.FindActionMap("Player");
        horizontalMovement = playerControlls.FindAction("HorizontalMovement");

        horizontalMovement.Enable();
    }

    private void OnDisable()
    {
        horizontalMovement.Disable();
    }

    private void Update()
    {
        // Get input value
        horizontalMovementValue = horizontalMovement.ReadValue<float>();

        ApplyHorizontalMovement();
    }

    private void ApplyHorizontalMovement()
    {
        float moveValue = horizontalMovementValue * speed * Time.deltaTime;
        transform.position += new Vector3(moveValue, 0, 0);
    }
}
