using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;  // To keep track of the pause state
    public string pauseMenuSceneName = "PauseMenu";  // The name of the pause menu scene
    private string currentGameSceneName;  // To store the name of the current game scene

    private bool canTogglePause = true;  // To check if the pause menu can be toggled
    public float pauseCooldown = 0.5f;  // Cooldown duration between pause toggles
    private float cooldownTimer = 0f;

    void Update()
    {
        // Handle cooldown timer
        if (!canTogglePause)
        {
            cooldownTimer -= Time.unscaledDeltaTime;  // Use unscaledDeltaTime because time is paused
            if (cooldownTimer <= 0f)
            {
                canTogglePause = true;  // Re-enable pause toggle when cooldown ends
            }
        }

        // Check for the ESC key press to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && canTogglePause)
        {
            if (isPaused)
            {
                ResumeGame();  // Resume if currently paused
            }
            else
            {
                PauseGame();  // Pause if not currently paused
            }

            canTogglePause = false;  // Disable further input
            cooldownTimer = pauseCooldown;  // Reset the cooldown timer
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        currentGameSceneName = SceneManager.GetActiveScene().name;  // Store the current game scene name
        SceneManager.LoadSceneAsync(pauseMenuSceneName, LoadSceneMode.Additive);  // Load the pause menu scene additively
        Time.timeScale = 0f;  // Freeze the game time
    }

    public void ResumeGame()
    {
        isPaused = false;
        SceneManager.UnloadSceneAsync(pauseMenuSceneName);  // Unload the pause menu scene
        Time.timeScale = 1f;  // Resume the game time
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;  // Ensure the game time is running before switching scenes
        SceneManager.LoadScene("MainMenu");  // Replace with your main menu scene name
    }

    public void ExitGame()
    {
        Application.Quit();  // Quit the application
    }
}
