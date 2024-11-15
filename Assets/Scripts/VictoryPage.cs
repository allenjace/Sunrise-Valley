using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("LastCompletedLevel", currentLevelIndex);
            PlayerPrefs.Save();
            Debug.Log("Level complete");
            SceneManager.LoadScene(10); // Victory screen
        }
    }
}