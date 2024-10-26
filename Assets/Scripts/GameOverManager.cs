using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float fallThreshold = -5f; // Y-position threshold for falling out of bounds

    void Update()
    {
        // Check if the player has fallen below the threshold
        if (player.position.y < fallThreshold)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        SceneReloader.SetPreviousScene();  // Store the previous level
        SceneManager.LoadSceneAsync("GameOver");
        // Stop time to pause the game
        Time.timeScale = 0f;
    }
}
