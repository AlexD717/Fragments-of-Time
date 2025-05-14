using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool playerOnPlatform;
    private bool playerOnPlatformLastFrame = false;
    private Vector3 lastFramePosition;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    private void Update()
    {
        if (playerOnPlatformLastFrame || playerOnPlatform)
        {
            player.transform.position += transform.position - lastFramePosition;
        }
        lastFramePosition = transform.position;
        playerOnPlatformLastFrame = playerOnPlatform;
    }
}
