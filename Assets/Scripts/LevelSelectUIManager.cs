using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelSelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject finalLevel;
    private UIManager uiManager;

    private int numLevels;
    private int currentSelectedLevel = 0;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();

        int maxLevelPassed = PlayerPrefsManager.GetMaxLevelPast();
        numLevels = Mathf.Clamp(menu.transform.childCount, 0, maxLevelPassed + 1);

        for (int i = 0; i < (numLevels); i++)
        {
            int levelNumber = i + 1;

            GameObject levelObject = menu.transform.GetChild(i).gameObject;

            if (levelObject != finalLevel)
            {
                // Add Listner to Load Level Button
                levelObject.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelNumber.ToString()));
                // Add Listner to Level Image
                levelObject.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelNumber.ToString()));

                // Change fastest pass time text
                levelObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefsManager.GetFastestLevelPassTimeText(i + 1);
            }
        }

        // Hide all level select menus
        foreach (Transform child in menu.transform)
        {
            child.gameObject.SetActive(false);
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