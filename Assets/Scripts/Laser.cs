using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public float speed;

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Collided with player
            collision.gameObject.GetComponent<Player>().InstaKillPlayer();
        }
        else if (collision.CompareTag("PlayerTimeTraveled"))
        {
            // Collided with time traveled player
            collision.gameObject.GetComponent<PlayerTimeTraveled>().InstaKill();
        }
        CollidedWithObject();
    }

    private void CollidedWithObject()
    {
        Destroy(gameObject);
    }
}
