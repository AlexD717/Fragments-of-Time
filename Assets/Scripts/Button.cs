using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject platform;

    private bool playerOnPlatform = false;

    public void playerEneterButton()
    {
        playerOnPlatform = true;
    }

    public void playerExitButton()
    {
        playerOnPlatform = false;
    }
}
