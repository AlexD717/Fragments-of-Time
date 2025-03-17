using UnityEngine;

public class NoDestoryOnLoadObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
