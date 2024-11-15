using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    public float wallJumpForce = 7f; // Force applied when wall jumping
    public float wallSlideSpeed = 2f; // Speed at which player slides down walls
    private float direction = 0f;
    private Rigidbody2D player;
    
    // Ground checking
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    
    // Wall checking
    public Transform wallCheckRight; // Add an empty GameObject as child on the right side
    public Transform wallCheckLeft;  // Add an empty GameObject as child on the left side
    public float wallCheckRadius = 0.2f;
    private bool isTouchingWallRight;
    private bool isTouchingWallLeft;
    private bool isWallSliding;
    
    // Coyote time for wall jumps
    public float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        CheckGroundAndWalls();
        HandleMovement();
        HandleWallSlide();
        HandleJumping();
    }
    
    void CheckGroundAndWalls()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWallRight = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer);
        isTouchingWallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, groundLayer);
        
        // Update wall sliding state
        isWallSliding = (isTouchingWallLeft || isTouchingWallRight) && !isTouchingGround && player.velocity.y < 0;
        
        // Update wall jump counter
        if (isWallSliding)
        {
            wallJumpCounter = wallJumpTime;
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }
    }
    
    void HandleMovement()
    {
        direction = Input.GetAxis("Horizontal");
        
        // Don't apply horizontal movement during wall jump
        if (!isWallSliding)
        {
            if (direction != 0f)
            {
                player.velocity = new Vector2(direction * speed, player.velocity.y);
            }
            else
            {
                player.velocity = new Vector2(0, player.velocity.y);
            }
        }
    }
    
    void HandleWallSlide()
    {
        if (isWallSliding)
        {
            player.velocity = new Vector2(player.velocity.x, Mathf.Max(player.velocity.y, -wallSlideSpeed));
        }
    }
    
    void HandleJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // Normal ground jump
            if (isTouchingGround)
            {
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            }
            // Wall jump
            else if (wallJumpCounter > 0)
            {
                // Determine jump direction based on which wall we're touching
                float wallJumpDirection = isTouchingWallRight ? -1f : 1f;
                
                // Apply wall jump force
                player.velocity = new Vector2(wallJumpDirection * wallJumpForce, jumpSpeed);
                
                // Reset wall jump counter
                wallJumpCounter = 0;
            }
        }
    }
}