using UnityEngine;

public class SeesawCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTimeTraveled"))
        {
            // Activates physics so that playerTimeTraveled can interact with the seesaw
            Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity);
            BoxCollider2D collisionCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            collisionCollider.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTimeTraveled"))
        {
            // Diactivate physics so that playerTimeTraveled is restored to noraml
            BoxCollider2D collisionCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            collisionCollider.isTrigger = true;
        }
    }
}
