using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1.0f;  // Time before the platform falls
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start the fall after a delay
            Invoke("Fall", fallDelay);
        }
    }

    void Fall()
    {
        // Change the Rigidbody2D to dynamic, so gravity affects it
        rb2D.isKinematic = false;
    }
}