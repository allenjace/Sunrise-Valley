using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolAndChase : MonoBehaviour
{
    public Transform player;          // Reference to the player's transform
    public float patrolSpeed = 2f;    // Speed at which the enemy patrols
    public float chaseSpeed = 4f;     // Speed at which the enemy chases the player
    public float patrolRange = 5f;    // Distance for patrolling (from the start position)
    public float detectionRange = 8f; // Range within which the enemy will detect the player
    private bool playerOnPlatform = false; // Whether the player is on the same platform
    private bool isChasing = false;   // Whether the enemy is chasing the player

    private Vector3 startPosition;    // Starting position for patrolling
    private Vector3 patrolTarget;     // Target position for patrol

    private bool movingRight = true;  // Check if enemy is moving right during patrol

    private void Start()
    {
        startPosition = transform.position; // Save the starting position for patrol
        SetPatrolTarget();
    }

    private void Update()
    {
        if (playerOnPlatform && !isChasing)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    // Detect when the player enters the platform (using trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = true; // Player is on the same platform
        }
    }

    // Detect when the player exits the platform (using trigger)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = false; // Player has left the platform
            isChasing = false; // Stop chasing when the player leaves the platform
        }
    }

    // Patrol between two points
    private void Patrol()
    {
        // Move the enemy between startPosition and patrolTarget
        transform.position = Vector2.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);

        // If the enemy reaches one of the patrol points, switch direction
        if (Vector2.Distance(transform.position, patrolTarget) < 0.1f)
        {
            movingRight = !movingRight;
            SetPatrolTarget();
        }

        // Flip the sprite to face the patrol direction
        FlipSprite(movingRight ? Vector2.right : Vector2.left);
    }

    // Set the patrol target based on the current direction
    private void SetPatrolTarget()
    {
        if (movingRight)
        {
            patrolTarget = new Vector3(startPosition.x + patrolRange, transform.position.y, transform.position.z);
        }
        else
        {
            patrolTarget = new Vector3(startPosition.x - patrolRange, transform.position.y, transform.position.z);
        }
    }

    // Chase the player when they are on the same platform
    private void ChasePlayer()
    {
        // Move towards the player's position
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        // Flip the sprite to face the player
        Vector2 direction = (player.position - transform.position).normalized;
        FlipSprite(direction);
    }

    // Flip the enemy sprite based on the direction of movement
    private void FlipSprite(Vector2 direction)
    {
        // Check if we need to flip the sprite based on the direction the enemy is facing
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            // Face right
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Invert the X-axis scale to flip the sprite
            transform.localScale = localScale;
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            // Face left
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Invert the X-axis scale to flip the sprite
            transform.localScale = localScale;
        }
    }
}
