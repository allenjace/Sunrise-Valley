using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SlideVolume: MonoBehaviour
{
    public Slider volumeSlider;       // Reference to the UI slider
    private const float multiplier = 30f;  // Used to convert linear slider value to logarithmic scale

    private void Start()
    {
        // Get current volume and set it to the slider
        float currentVolume = GetVolume();
        volumeSlider.value = currentVolume;

        // Add listener to the slider to handle value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Method to get the current volume
    private float GetVolume()
    {
        //get volume from AudioListener (default system volume)
        return AudioListener.volume;
    }

    // Method to set the volume when the slider value changes
    public void SetVolume(float volume)
    {
            // Set the volume on AudioListener (for global audio)
        AudioListener.volume = volume;
    }

    private void OnDestroy()
    {
        // Clean up listener when the object is destroyed
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
