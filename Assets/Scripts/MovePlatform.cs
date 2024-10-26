using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed at which the platform moves
    public float leftBound = -5f;  // Leftmost X position
    public float rightBound = 5f;  // Rightmost X position

    private bool movingRight = true;  // Is the platform moving right?

    void Update()
    {
        // Move the platform
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);  // Move right
            if (transform.position.x >= rightBound)  // If we reached the right bound
            {
                movingRight = false;  // Switch direction to move left
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);  // Move left
            if (transform.position.x <= leftBound)  // If we reached the left bound
            {
                movingRight = true;  // Switch direction to move right
            }
        }
    }
}
