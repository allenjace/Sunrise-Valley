using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerMoveSpeed = 5f;
    public float slopeCheckDistance = 0.5f; // Distance to check for slopes
    public LayerMask groundLayer; // Set the ground layer for slopes and flat ground

    private Vector2 moveInput;
    public bool PlayerMoving { get; private set; }
    private Rigidbody2D rb;
    private bool facingRight = true; // To track the direction the player is facing

    // Slope Handling Variables
    private bool isOnSlope = false;
    private float slopeAngle;
    private Vector2 slopeNormalPerpendicular;
    public float maxSlopeAngle = 45f; // Maximum angle the player can walk up

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FlipPlayer(); // Call flip logic in Update to check movement direction
    }

    private void FixedUpdate()
    {
        CheckSlope();
        MovePlayer();
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        PlayerMoving = moveInput != Vector2.zero;
    }

    private void MovePlayer()
    {
        if (isOnSlope && slopeAngle <= maxSlopeAngle)
        {
            // Move along the slope
            rb.velocity = new Vector2(-moveInput.x * PlayerMoveSpeed * slopeNormalPerpendicular.x, moveInput.x * PlayerMoveSpeed * slopeNormalPerpendicular.y);
        }
        else
        {
            // Move normally on flat ground or when not on a slope
            rb.velocity = new Vector2(moveInput.x * PlayerMoveSpeed, rb.velocity.y);
        }
    }

    private void CheckSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, groundLayer);
        if (hit)
        {
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeAngle != 0)
            {
                isOnSlope = true;
            }
            else
            {
                isOnSlope = false;
            }
        }
        else
        {
            isOnSlope = false;
        }
    }

    private void FlipPlayer()
    {
        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Flip the player's local scale on the X-axis
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
