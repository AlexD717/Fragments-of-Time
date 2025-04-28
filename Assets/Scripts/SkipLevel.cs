using System.Runtime.CompilerServices;
using UnityEngine;

public class SkipLevel : MonoBehaviour
{
    [SerializeField] private int restartesToShow;

    private void Start()
    {
        int timesRestarted = FindFirstObjectByType<PlayerTimeManager>().timesRestarted;
        if (timesRestarted >= restartesToShow)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SkipThisLevel()
    {
        NextLevelPortal nextLevelPortal = FindFirstObjectByType<NextLevelPortal>();
        nextLevelPortal.SkipThisLevel();
    }
}
