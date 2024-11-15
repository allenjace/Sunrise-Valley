using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    AudioSource source;
    Collider2D collider2D;
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
        collider2D = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            source.Play();
        }
    }
}
