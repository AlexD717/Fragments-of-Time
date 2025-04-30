using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public GameObject parentSpawner; // Parent object that spawned this
    [HideInInspector] public float timeAlive;

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        if (timeAlive == 0) { timeAlive = 8f; }
        Invoke("CollidedWithObject", timeAlive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parentSpawner)
        {
            return;
        }
        else if (collision.CompareTag("Player"))
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
        Debug.Log("CollidedWithObject");
        Destroy(gameObject);
    }
}
