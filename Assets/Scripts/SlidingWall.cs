using UnityEngine;

public class SlidingWall : MonoBehaviour
{
    [SerializeField] private ButtonPlatform buttonPlatform;
    private float originalXScale;
    [SerializeField] private float pressedXScale;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 moveAmounts;

    private void Start()
    {
        originalXScale = transform.localScale.x;
    }

    private void Update()
    {
        UpdateWallScale(buttonPlatform.IsButtonPressed());
    }

    private void UpdateWallScale(bool buttonPressed)
    {
        float targetXScale;
        if (buttonPressed) { targetXScale = pressedXScale; }
        else { targetXScale = originalXScale; }

        float xScale = transform.localScale.x;

        if (xScale != targetXScale)
        {
            float xScaleChange = Mathf.Abs(xScale - targetXScale) * Time.deltaTime * speed;
            if (targetXScale < xScaleChange)
            {
                xScaleChange = -xScaleChange;
            }

            transform.localScale += new Vector3(xScaleChange, 0, 0);
            transform.position += new Vector3(xScaleChange / 2f * moveAmounts.x, -xScaleChange / 2f * moveAmounts.y, 0);
        }
    }
}
