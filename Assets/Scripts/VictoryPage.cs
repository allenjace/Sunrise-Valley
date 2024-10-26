using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPage : MonoBehaviour
{
    public int currentLevelIndex = 6; // Set this to the current level's index (6, 7, or 8)
    private SaveLoad levelManager; // Reference to LevelManager

    void Start()
    {
        // Find the LevelManager in the scene
        levelManager = FindObjectOfType<SaveLoad>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call LevelManager to save the level as completed
            if (levelManager != null)
            {
                levelManager.SaveLevelCompletion(currentLevelIndex);
                levelManager.UpdateLevelButtons();
            }
            VictoryScript.GetNextScene();
            // Load the victory scene
            SceneManager.LoadSceneAsync("Victory");
        }
    }
}
