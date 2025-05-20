using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] PauseManager pauseManager;

    public void HideAnimationFinished()
    {
        pauseManager.UnpauseTime();
    }
}