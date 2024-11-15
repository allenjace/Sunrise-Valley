using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelProgressManager : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        // Check if we're in the level select screen
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("Updating button colors..."); 
            UpdateLevelButtonColors();
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        Debug.Log($"Completing level {levelIndex}"); 
        // We don't need to set PlayerPrefs here as it's already set in LevelCompleteTrigger
        SceneManager.LoadScene(10); // Victory screen
    }

    private void UpdateLevelButtonColors()
    {
        if (levelButtons == null || levelButtons.Length == 0)
        {
            Debug.LogError("Level buttons array is empty or null!");
            return;
        }

        int lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        Debug.Log($"Last completed level: {lastCompletedLevel}");

        // Level indices are 1, 2, 3, 4
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] == null)
            {
                Debug.LogError($"Button at index {i} is null!");
                continue;
            }

            int levelIndex = i + 1; // Convert button index to level index
            bool isCompleted = IsLevelCompleted(levelIndex, lastCompletedLevel);
            Debug.Log($"Level {levelIndex} completion status: {isCompleted}");

            if (isCompleted)
            {
                ColorBlock colors = levelButtons[i].colors;
                Color greenColor = new Color(0, 1, 0, 1); // Bright green
                
                colors.normalColor = greenColor;
                colors.highlightedColor = new Color(0, 0.8f, 0, 1);
                colors.pressedColor = new Color(0, 0.6f, 0, 1);
                colors.selectedColor = new Color(0, 0.8f, 0, 1);
                colors.disabledColor = new Color(0, 0.6f, 0, 0.5f);
                
                levelButtons[i].colors = colors;

                Image buttonImage = levelButtons[i].GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = greenColor;
                }
            }
        }
    }

    private bool IsLevelCompleted(int levelIndex, int lastCompletedLevel)
    {
        // A level is completed if its index is less than or equal to the last completed level
        bool isCompleted = levelIndex <= lastCompletedLevel;
        Debug.Log($"Checking level {levelIndex} completion: {isCompleted}"); 
        return isCompleted;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DebugPrintProgress()
    {
        int lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        Debug.Log($"Last Completed Level: {lastCompletedLevel}");
        
        for (int i = 2; i <= 5; i++)
        {
            Debug.Log($"Level {i} status: {(i <= lastCompletedLevel ? "Completed" : "Not Completed")}");
        }
    }
}