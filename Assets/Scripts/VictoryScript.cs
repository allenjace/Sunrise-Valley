using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    private static int sceneIndex;

    private void Start()
    {
        GetNextScene();
    }

    public static void GetNextScene()
    {
        sceneIndex = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        Debug.Log($"Getting next scene. Last completed level index: {sceneIndex}");
        
        if (sceneIndex >= 2 && sceneIndex <= 5)
        {
            FindObjectOfType<LevelProgressManager>().CompleteLevel(sceneIndex);
        }
    }

    public void NextLevel()
    {
        Debug.Log($"Current scene index: {sceneIndex}");
        int nextSceneIndex = sceneIndex + 1;
        
        if (sceneIndex >= 2 && sceneIndex < 5)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else if (sceneIndex == 5)
        {
            SceneManager.LoadScene(0); // Main menu
        }
    }
}