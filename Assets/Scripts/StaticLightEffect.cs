using UnityEngine;

public class StaticLightEffect : MonoBehaviour
{
    [Header("Flicker Settings")]
    private float flickerTimer;
    [SerializeField] private Light flashlight;  // The Light component
    public float minIntensity = 0.8f;  // Minimum light intensity
    public float maxIntensity = 0.9f;  // Maximum light intensity
    public float flickerSpeed = 0.01f;  // Time between flickers (in seconds)

    private void Update()
    {
        FlickerLight();
    }

    void FlickerLight()
    {
        // Countdown timer
        flickerTimer -= Time.deltaTime;

        if (flickerTimer <= 0f)
        {
            // Set a random intensity within range
            flashlight.intensity = Random.Range(minIntensity, maxIntensity);

            // Reset the timer
            flickerTimer = flickerSpeed;
        }
    }
}
