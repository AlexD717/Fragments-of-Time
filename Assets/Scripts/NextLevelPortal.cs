using UnityEngine;

public class NextLevelPortal : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float speedAtWhichToHid;

    [Header("References")]
    [SerializeField] private SpriteRenderer hideEverythingSprite;
    [SerializeField] private GameObject glowLight;

    private bool hideEverything = false;
    private bool menuShown = false;
    private GameManager gameManager;

    private void Start()
    {
        glowLight.SetActive(true);
        hideEverythingSprite.gameObject.SetActive(false);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.LevelPast();
            hideEverything = true;
            glowLight.SetActive(false);
            hideEverythingSprite.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (hideEverything)
        {
            if (hideEverythingSprite.color.a < 1)
            {
                hideEverythingSprite.color += new Color(0, 0, 0, speedAtWhichToHid * Time.unscaledDeltaTime);
            }
            if (hideEverythingSprite.color.a > 0.98)
            {
                if (!menuShown)
                {
                    menuShown = true;
                    gameManager.ShowWinMenu();
                }
            }
        }
    }

}
