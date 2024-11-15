using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LifeCherryManager : MonoBehaviour
{
    public GameObject[] hearts;
    private int lives;
    public Transform player;
    public Vector3 offset = new Vector3(1f, 1f, 0f);
    public TextMeshProUGUI cherryText;
    private int cherryCount = 0;
    public Vector3 cherryTextOffset = new Vector3(2f, 2f, 0f);
    public int maxLives = 3; // Maximum number of lives allowed

    void Start()
    {
        lives = hearts.Length;
        UpdateText();
    }

    void Update()
    {
        // Make hearts follow player
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].transform.position = player.position + offset + new Vector3(i * 1.0f, 0f, 0f);
            }
        }

        // Make cherry counter follow player
        if (cherryText != null)
        {
            cherryText.transform.position = Camera.main.WorldToScreenPoint(player.position + cherryTextOffset);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Trap"))
        {
            DecreaseLife();
        }
        else if (other.CompareTag("Cherry"))
        {
            CollectCherries();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ExtraLife")) // Add this tag to life pickup items
        {
            IncreaseLife();
            Destroy(other.gameObject);
        }
    }

    void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;
            if (hearts[lives] != null)
            {
                hearts[lives].SetActive(false); // Hide the heart instead of destroying it
            }
            
            if (lives <= 0)
            {
                SceneManager.LoadSceneAsync("GameOver");
            }
        }
    }

    void IncreaseLife()
    {
        if (lives < maxLives)  // Check if below max lives
        {
            if (hearts[lives] != null)
            {
                hearts[lives].SetActive(true); // Show the heart
                lives++;
            }
        }
    }

    void CollectCherries()
    {
        cherryCount++;
        UpdateText();
    }

    void UpdateText()
    {
        cherryText.text = "Cherries: " + cherryCount.ToString();
    }
}