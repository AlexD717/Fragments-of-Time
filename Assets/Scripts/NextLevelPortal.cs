using UnityEngine;
using UnityEngine.UI;

public class NextLevelPortal : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float speedAtWhichToHid;

    [Header("References")]
    [SerializeField] private Image hideEverythingImage;
    [SerializeField] private GameObject glowLight;

    private bool hideEverything = false;
    private bool menuShown = false;
    private GameManager gameManager;
    private bool levelSkipped = false;

    private void Start()
    {
        glowLight.SetActive(true);
        hideEverythingImage.gameObject.SetActive(false);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelPast();
        }
    }

    private void LevelPast()
    {
        gameManager.LevelPast();
        hideEverything = true;
        glowLight.SetActive(false);
        hideEverythingImage.gameObject.SetActive(true);
    }

    public void SkipThisLevel()
    {
        LevelPast();
        levelSkipped = true;
    }

    private void Update()
    {
        if (hideEverything)
        {
            if (hideEverythingImage.color.a < 1)
            {
                hideEverythingImage.color += new Color(0, 0, 0, speedAtWhichToHid * Time.unscaledDeltaTime);
            }
            if (hideEverythingImage.color.a > 0.98)
            {
                if (!menuShown)
                {
                    menuShown = true;
                    gameManager.ShowWinMenu(levelSkipped);
                }
            }
        }
    }

}
