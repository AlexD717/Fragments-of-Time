using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public float timeLeftGround;

    private void Start()
    {
        timeLeftGround = Time.time;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = collision != null && (((1 << collision.gameObject.layer) & groundLayerMask) != 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        timeLeftGround = Time.time;
    }
}