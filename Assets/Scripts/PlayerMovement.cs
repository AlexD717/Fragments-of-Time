using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionAsset inputSystem;
    private InputAction horizontalMovementAction;
    private InputAction jumpAction;
    private float horizontalMovementValue;

    private Rigidbody2D rb;
    [SerializeField] private GroundCheck groundCheck;

    private enum AnimationStates
    {
        Idle,
        Running,
        Jumping,
    }
    private AnimationStates animationState;

    private void OnEnable()
    {
        // Find input actions
        var playerControlls = inputSystem.FindActionMap("Player");
        horizontalMovementAction = playerControlls.FindAction("HorizontalMovement");
        jumpAction = playerControlls.FindAction("Jump");

        horizontalMovementAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        horizontalMovementAction.Disable();
        jumpAction.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationState = AnimationStates.Idle;
    }

    private void Update()
    {
        // Get input value
        horizontalMovementValue = horizontalMovementAction.ReadValue<float>();

        // Rotate character to face correct direction
        if (horizontalMovementValue > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalMovementValue < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Check if jump triggered, and jump if needed
        if (jumpAction.triggered)
        {
            Jump();
        }

        ApplyHorizontalMovement();

        UpdateAnimation();
    }

    private void Jump()
    {
        if (groundCheck.isGrounded || Time.time - groundCheck.timeLeftGround < coyoteTime)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyHorizontalMovement()
    {
        float moveValue = horizontalMovementValue * speed * Time.deltaTime;
        transform.position += new Vector3(moveValue, 0, 0);
    }

    private void UpdateAnimation()
    {
        bool isGrounded = groundCheck.isGrounded;
        if (!isGrounded)
        {
            animationState = AnimationStates.Jumping;
        }
        else if (horizontalMovementValue == 0)
        {
            animationState = AnimationStates.Idle;
        }
        else
        {
            animationState = AnimationStates.Running;
        }

        animator.SetInteger("state", GetAnimationState());
    }

    public int GetAnimationState()
    {
        return (int)animationState;
    }
}