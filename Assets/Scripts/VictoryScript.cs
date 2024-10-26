using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    public static int sceneIndex;

    public static void GetNextScene()
    {
        // Store the current active scene's build index
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void NextLevel()
    {
        int nextSceneIndex = sceneIndex + 1;
        // Check if the current scene is within the custom range
        if (sceneIndex >= 6 && sceneIndex < 8)
        {
            // Load the next level in the custom range
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        else if (sceneIndex == 8)
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}