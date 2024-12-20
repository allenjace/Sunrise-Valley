using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // Store the previous scene's build index
    public static int previousSceneIndex;

    public void RestartCurrentScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1f;
    }

    // Call this method before loading the Game Over screen
    public static void SetPreviousScene()
    {
        // Store the current active scene's build index
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Method to restart the previous scene when the player clicks the "Restart" button
    public void RestartPreviousScene()
    {
        // Check if the previous scene is set to the correct level index range
        if (previousSceneIndex >= 7 && previousSceneIndex <= 11)
        {
            SceneManager.LoadScene(previousSceneIndex);  // Restart the previous level
            Time.timeScale = 1f;
        }
    }
}