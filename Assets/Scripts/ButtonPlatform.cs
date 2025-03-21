using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 yPositions;
    [SerializeField] private float speed;
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Button button;
    private bool playerOnButton = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundCheck")
        {
            button.playerEneterButton();
            playerOnButton = true;
            spriteRenderer.color = greenColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundCheck")
        {
            button.playerExitButton();
            playerOnButton = false;
            spriteRenderer.color = redColor;
        }
    }

    private void Update()
    {
        float targetYPos;
        if (!playerOnButton)
        {
            targetYPos = yPositions.x;
        }
        else 
        {
            targetYPos = yPositions.y;
        }

        if (transform.localPosition.y != targetYPos)
        {
            float newYPos =  ((transform.localPosition.y - targetYPos) * Time.deltaTime * speed);
            transform.localPosition -= new Vector3(0, newYPos, 0);
        }
    }
}
