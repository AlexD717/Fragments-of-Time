using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void LoadLevel(string levelNum)
    {
        Loader.Load("Level"+levelNum);
    }
}
