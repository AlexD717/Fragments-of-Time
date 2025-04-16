using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelSelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private UIManager uiManager;

    private int numLevels;
    private int currentSelectedLevel = 0;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();

        int maxLevelPassed = PlayerPrefsManager.GetMaxLevelPast();
        numLevels = Mathf.Clamp(menu.transform.childCount, 0, maxLevelPassed + 1); 

        for (int i = 0; i < numLevels; i++)
        {
            int levelNumber = i + 1;
            
            // Add Listner to Load Level Button
            menu.transform.GetChild(i).GetChild(1).GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelNumber.ToString()));
        
            // Change fastest pass time text
            menu.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefsManager.GetFastestLevelPassTimeText(i+1);
        }

        currentSelectedLevel = numLevels - 1;
        SelectLevel(currentSelectedLevel);
        if (maxLevelPassed < numLevels)
        {
            menu.transform.GetChild(currentSelectedLevel).GetChild(0).GetComponent<Image>().color -= new Color(0, 0, 0, 0.9f);
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
