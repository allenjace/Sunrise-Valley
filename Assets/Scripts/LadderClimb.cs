using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float climbSpeed = 5f;     // Speed at which the player climbs
    private bool isClimbing = false;  // Check if the player is currently on a ladder
    private Rigidbody2D rb;           // Reference to the player's Rigidbody2D
    private float defaultGravity;     // Store the default gravity scale of the player

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale; // Save the player's default gravity scale
    }

    private void Update()
    {
        if (isClimbing)
        {
            // Get the player's vertical input (up/down)
            float verticalInput = Input.GetAxis("Vertical");

            // Move the player vertically while climbing the ladder
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);

            // Disable gravity while climbing so the player doesn't fall off the ladder
            rb.gravityScale = 0f;
        }
        else
        {
            // Re-enable the player's gravity when not climbing
            rb.gravityScale = defaultGravity;
        }
    }

    // Detect when the player enters the ladder's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    // Detect when the player exits the ladder's collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}
