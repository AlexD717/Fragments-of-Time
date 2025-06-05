using UnityEngine;
using System.Collections;

public class FinalDecision : MonoBehaviour
{
    [SerializeField] private float speedToHideAt;
    
    [SerializeField] private GameObject playerSavedWorldScreen;
    [SerializeField] private GameObject playerKeptPowerScreen;
    [SerializeField] private SpriteRenderer screenShatterHider;

    private InfoTextManager infoTextManager;

    private void Start()
    {
        infoTextManager = GetComponent<InfoTextManager>();
    }
    
    public void PlayerSavesWorld()
    {
        Debug.Log("Player decided to save the world");
        PlayerDecided();
        StartCoroutine(SetAlphaToMax());
    }

    private IEnumerator SetAlphaToMax()
    {
        // Hides screen shatters
        while (screenShatterHider.color.a < 1f)
        {
            screenShatterHider.color += new Color(0, 0, 0, 0.05f);
            yield return new WaitForSecondsRealtime(speedToHideAt);
        }
        Debug.Log("Screen Shatters Hidden");
        Invoke("ShowPlayerSavedWorldScreen", 1f);
    }

    private void ShowPlayerSavedWorldScreen()
    {
        playerSavedWorldScreen.SetActive(true);
    }

    public void PlayerKeepsPower()
    {
        Debug.Log("Player decided to keep the power");
        PlayerDecided();
        Invoke("ShowPlayerKeptPowerScreen", 1f);
    }

    private void ShowPlayerKeptPowerScreen()
    {
        playerKeptPowerScreen.SetActive(true);
    }

    private void PlayerDecided()
    {
        infoTextManager.PlayerDecided(-1);
    }
}