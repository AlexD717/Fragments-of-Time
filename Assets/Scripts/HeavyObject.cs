using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeavyObject : MonoBehaviour
{
    [SerializeField] private float velocityAtWhichToKill;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 velocity = rb.linearVelocity;
            if (velocity.magnitude >= velocityAtWhichToKill)
            {
                collision.gameObject.GetComponent<Player>().InstaKillPlayer();
            }
        }
    }
}