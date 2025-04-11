using UnityEngine.UI;
using UnityEngine;

public class LevelSelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private UIManager uiManager;

    private int numLevels;
    private int currentSelectedLevel = 0;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();

        numLevels = menu.transform.childCount;

        for (int i = 0; i < numLevels; i++)
        {
            int levelNumber = i + 1;
            menu.transform.GetChild(i).GetChild(1).GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelNumber.ToString()));
        }
    }

    private void LoadLevel(string levelNum)
    {
        uiManager.LoadLevel(levelNum);
    }

    public void NextLevel()
    {
        currentSelectedLevel++;
        currentSelectedLevel = currentSelectedLevel % numLevels;
        SelectLevel(currentSelectedLevel);
    }

    public void PreviousLevel()
    {
        if (currentSelectedLevel > 0)
        {
            currentSelectedLevel--;
        }
        else
        {
            currentSelectedLevel = numLevels - 1;
        }
        SelectLevel(currentSelectedLevel);
    }

    private void SelectLevel(int levelIndex)
    {
        for (int i = 0; i < numLevels; i++)
        {
            if (levelIndex == i)
            {
                menu.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                menu.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
