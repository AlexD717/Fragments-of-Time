using UnityEngine;

public class Player : MonoBehaviour
{
    public void InstaKillPlayer()
    {
        Destroy(gameObject);
        PlayerTimeManager playerTimeManager = FindFirstObjectByType<PlayerTimeManager>();
        if (playerTimeManager != null ) { playerTimeManager.TimeTravelRestart(); }
    }
}
