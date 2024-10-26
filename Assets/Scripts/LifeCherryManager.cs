using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LifeCherryManager : MonoBehaviour
{
    public GameObject[] hearts;  // Array to hold the heart GameObjects
    private int lives;  // Number of lives the player has
    public Transform player;  // Player transform to follow
    public Vector3 offset = new Vector3(1f, 1f, 0f);  // Offset from the player for the hearts
    public TextMeshProUGUI cherryText;  // UI Text to display the number of cherries collected
    private int cherryCount = 0;  // Counter for the cherries
    public Vector3 cherryTextOffset = new Vector3(2f, 2f, 0f);

    void Start()
    {
        lives = hearts.Length;  // Set lives to the number of heart GameObjects
        UpdateText();  // Initialize the cherry counter UI
    }
    void Update()
    {
        // Make the hearts follow the player
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].transform.position = player.position + offset + new Vector3(i * 1.0f, 0f, 0f);  // Adjust the offset as needed
            }
        }

        // Make the cherry counter UI follow the player
        if (cherryText != null)
        {
            cherryText.transform.position = Camera.main.WorldToScreenPoint(player.position + cherryTextOffset);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || (other.CompareTag("Trap"))) // Check if the player touches a hazard
        {
            DecreaseLife();
        }
        else if (other.CompareTag("Cherry"))  // Check if the player collects a cherry
        {
            CollectCherries();
            Destroy(other.gameObject);  // Destroy the cherry GameObject
        }
    }
    void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;  // Decrease the lives count
            Destroy(hearts[lives]);  // Destroy the corresponding heart GameObject
            if (lives <= 0)
            {
                // Handle game over, player death, etc.
                SceneManager.LoadSceneAsync("GameOver");
            }
        }
    }

    void CollectCherries()
    {
        cherryCount++;  // Increase the cherry count
        UpdateText();  // Update the UI to reflect the new cherry count
    }

    void UpdateText()
    {
        cherryText.text = "Cherries: " + cherryCount.ToString();  // Update the TextMeshProUGUI with the cherry count
    }
}