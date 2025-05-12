using UnityEngine;

public class HighJumpPlatformObject : MonoBehaviour
{
    [SerializeField] private float pushForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRB.linearVelocityY = 0;
            playerRB.AddForceY(pushForce);
        }
    }
}
