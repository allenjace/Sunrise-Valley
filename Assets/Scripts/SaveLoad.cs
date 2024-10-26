using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public Button[] levelButtons;
    public Slider volumeSlider;
    public int currentLevelIndex = 6;
    public void SaveLevelCompletion(int levelIndex)
    {      
        PlayerPrefs.SetInt("Level_" + levelIndex, 1); // 1 means completed
        PlayerPrefs.Save(); // This ensures the data is written immediately
    }
    public bool IsLevelCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt("Level_" + levelIndex, 0) == 1; // 0 means not completed
    }

    public void SaveProgress()
    {
        SaveLevelCompletion(currentLevelIndex);
        if (volumeSlider != null)
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value); // Save the current volume value
            PlayerPrefs.Save();
        }
    }
    public void LoadProgress()
    {
        UpdateLevelButtons();
        if (volumeSlider != null && PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = savedVolume; // Set the slider to the saved volume value
        }
    }
    public void UpdateLevelButtons()
    {
        // Ensure that the number of buttons corresponds to the number of levels (3 in this case)
        if (levelButtons.Length != 3)
        {
            return;
        }

        // Loop through the levels (6 to 8, inclusive)
        for (int i = 6; i <= 8; i++) 
        {
            bool isCompleted = IsLevelCompleted(i);

            // i - 6 maps level index (6, 7, 8) to array index (0, 1, 2)
            int buttonIndex = i - 6;

            if (buttonIndex >= 0 && buttonIndex < levelButtons.Length)
            {
                if (i == 6) // First level is always interactable
                {
                    levelButtons[buttonIndex].interactable = true;
                }
                else
                {
                    // Enable the button only if the previous level is completed
                    levelButtons[buttonIndex].interactable = isCompleted;
                }
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UpdateLevelButtons(); // Update the button states when the scene starts
        if (volumeSlider != null && PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = savedVolume;
        }   
    }
}