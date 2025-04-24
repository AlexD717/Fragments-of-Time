using UnityEngine;
using UnityEngine.UI;

public class ScreenShatterAnimation : MonoBehaviour
{
    [SerializeField] private Vector2 screenShatterWidth;
    [SerializeField] private int numTotalShatters;
    [SerializeField] private GameObject screenShatterPrefab;
    [SerializeField] private Button[] buttonToDeactivate;
    private bool buttonsDisabled = false;

    [SerializeField] private float volumeReductionNum;
    [SerializeField] private float volumeReductionMult;

    private bool screenShatterAnimStarted = false;
    private int numShatters = 0;
    [SerializeField] private float startingTime;
    [SerializeField] private float r;
    private float nextShatterTime = 0f;

    private string nextLevelToLoad;

    [SerializeField] private RectTransform canvas;

    private void Update()
    {
        if (!screenShatterAnimStarted) return;
        if (!buttonsDisabled)
        {
            foreach (Button button in buttonToDeactivate)
            {
                button.enabled = false;
            }
            buttonsDisabled = true;
        }


        if (Time.time >= nextShatterTime)
        {
            numShatters++;
            SpawnScreenShatter();
            if (numShatters >= numTotalShatters)
            {
                Loader.LoadByName(nextLevelToLoad);
            }
            
            nextShatterTime = Time.time + Mathf.Pow(startingTime*(1-r), numShatters);
        }
    }

    public void ShatterScreen(string level)
    {
        screenShatterAnimStarted = true;
        nextLevelToLoad = level;
    }

    private void SpawnScreenShatter()
    {
        // Gets a random point on the canvas 
        Vector3 screenPosition = new Vector3(Random.Range(0, canvas.rect.width/2), Random.Range(0, canvas.rect.height/2), 0);

        GameObject screenShatter = Instantiate(screenShatterPrefab, screenPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        screenShatter.transform.SetParent(canvas, true);
        RectTransform rectTransform = screenShatter.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, Random.Range(screenShatterWidth.x, screenShatterWidth.y));

        // Reduce volume when to many shatters
        if (numShatters >= volumeReductionNum)
        {
            screenShatter.GetComponent<AudioSource>().volume *= volumeReductionMult;
        }
    }
}