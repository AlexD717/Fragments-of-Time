using TMPro;
using UnityEngine;

public class LoadingTextAnimation : MonoBehaviour
{
    [SerializeField] private float dotsAddingDelay;
    [SerializeField] private int numDots = 3;
    private float nextDotAddTime = 0;
    private TextMeshProUGUI loadingText;
    private string baseString;

    private void Start()
    {
        loadingText = GetComponent<TextMeshProUGUI>();
        baseString = loadingText.text;
    }

    private void Update()
    {
        if (Time.time >= nextDotAddTime)
        {
            nextDotAddTime = Time.time + dotsAddingDelay;
            if (loadingText.text.Length >= baseString.Length + numDots)
            {
                loadingText.text = baseString;
            }
            else
            {
                loadingText.text = loadingText.text + ".";
            }
        }
    }
}