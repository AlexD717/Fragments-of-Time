using UnityEngine;
using System.Collections.Generic;


public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 yPositions;
    [SerializeField] private float speed;
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;
    private bool playerOnButton = false;
    [HideInInspector] public bool buttonPressed = false;
    private List<bool> timesOn;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float timeToBePressed;
    private float timeWhenButtonPressed = Mathf.Infinity;

    private void Awake()
    {
        Debug.Log("Times On Cleared");
        timesOn = new List<bool>();
    }

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
            playerOnButton = false;
            spriteRenderer.color = redColor;
            timeWhenButtonPressed = Mathf.Infinity;
            buttonPressed = false;
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(timesOn.Count);
        bool pressedByTimeTraveledPlayer = false;
        if (timesOn.Count > 0)
        {
            pressedByTimeTraveledPlayer = timesOn[0];
            Debug.Log(pressedByTimeTraveledPlayer);
            timesOn.RemoveAt(0);
        }
        if (Time.time > timeWhenButtonPressed || pressedByTimeTraveledPlayer)
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

    public void SetTimesOn(List<bool> _timesOn)
    {
        Debug.Log("Setting Times On");
        timesOn = new List<bool>(_timesOn);
        Debug.Log(timesOn.Count);
    }
}
