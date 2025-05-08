using UnityEngine;

public class LiftObject : MonoBehaviour
{
    [SerializeField] private float gravityScale;
    [SerializeField] private AnimationCurve gravityMod;
    private GameObject player;
    private float endLocation;
    private float startLocation;
    private float liftDistance;
    private float startingGravity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        startingGravity = playerRb.gravityScale;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        endLocation = transform.position.y + boxCollider.offset.y + boxCollider.size.y / 2;
        startLocation = transform.position.y + boxCollider.offset.y - boxCollider.size.y / 2;
        liftDistance = endLocation - startLocation;
        Debug.Log(endLocation);
        Debug.Log(startLocation);
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerPositionInLift = Mathf.Clamp((player.transform.position.y - startLocation) / liftDistance, 0f, 1f);
            float gravityModifier = Mathf.Clamp(gravityMod.Evaluate(playerPositionInLift), 0f, 1f);
            Debug.Log(gravityModifier);
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale * gravityModifier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRB.gravityScale = startingGravity;
            playerRB.linearVelocity = Vector2.zero;
        }
    }
}
