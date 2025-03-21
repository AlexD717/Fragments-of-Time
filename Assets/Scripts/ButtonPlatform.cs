using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 yPositions;
    [SerializeField] private float speed;
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Button button;
    private bool playerOnButton = false;
    private bool buttonPressed = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float timeToBePressed;
    private float timeWhenButtonPressed = Mathf.Infinity;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundCheck")
        {
            playerOnButton = true;
            timeWhenButtonPressed = Time.time + timeToBePressed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundCheck")
        {
            button.playerExitButton();
            playerOnButton = false;
            spriteRenderer.color = redColor;
            timeWhenButtonPressed = Mathf.Infinity;
            button.playerExitButton();
            buttonPressed = false;
        }
    }

    private void Update()
    {
        if (Time.time > timeWhenButtonPressed)
        {
            ButtonPressed();
        }
        UpdatePlatformPosition();
    }

    private void ButtonPressed()
    {
        buttonPressed = true;
        button.playerEneterButton();
        spriteRenderer.color = greenColor;
    }

    private void UpdatePlatformPosition()
    {
        float targetYPos;
        if (buttonPressed) { targetYPos = yPositions.y;  }
        else { targetYPos = yPositions.x; }

        if (transform.localPosition.y != targetYPos)
        {
            float newYPos = ((transform.localPosition.y - targetYPos) * Time.deltaTime * speed);
            transform.localPosition -= new Vector3(0, newYPos, 0);
        }
    }
}
