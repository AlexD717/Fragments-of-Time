using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    [SerializeField] private bool dontDestroyOnLoad;
    [SerializeField] private float destroyDelayTime;

    private void Start()
    {
        if (dontDestroyOnLoad)
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }
        Destroy(gameObject, destroyDelayTime);
    }
}
