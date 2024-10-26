using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Reference to the player's transform
    public Vector3 offset;        // Offset between the player and camera
    public float smoothSpeed = 0.125f;  // How smoothly the camera will follow the player

    private void FixedUpdate()
    {
        // Target position the camera should move towards
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
