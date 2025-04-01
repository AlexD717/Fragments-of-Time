using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 yPositions;
    [SerializeField] private float speed;
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;
    private float playersOnButton = 0;
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
            playersOnButton += 1;
            timeWhenButtonPressed = Time.time + timeToBePressed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundCheck")
        {
            playersOnButton -= 1;
            if (playersOnButton == 0)
            {
                timeWhenButtonPressed = Mathf.Infinity;
                buttonPressed = false;
                spriteRenderer.color = redColor;
            }
        }
    }

    private void Update()
    {
        if (Time.time > timeWhenButtonPressed && playersOnButton > 0)
        {
            ButtonPressed();
        }
        UpdatePlatformPosition();
    }

    private void ButtonPressed()
    {
        buttonPressed = true;
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

    public bool IsButtonPressed()
    {
        return buttonPressed;
    }
}
