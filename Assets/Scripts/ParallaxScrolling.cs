using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam; // Reference to the main camera
    public float parallaxEffectMultiplier;

    void Start()
    {
        startpos = transform.position.x; // The starting position of the background element
        length = GetComponent<SpriteRenderer>().bounds.size.x; // The width of the background element
    }

    void Update()
    {
        // The parallax effect calculation: difference between camera's starting position and current position
        float temp = (cam.transform.position.x * (1 - parallaxEffectMultiplier));
        float dist = (cam.transform.position.x * parallaxEffectMultiplier);
        
        // Move the background at a speed relative to the camera
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        
        // If the camera moves far enough, the background wraps around (useful for endless side-scrollers)
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}

