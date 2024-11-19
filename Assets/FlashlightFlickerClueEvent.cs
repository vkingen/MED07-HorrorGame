using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class FlashlightFlickerClueEvent : MonoBehaviour
{
    private bool isFlickering = false;
    [SerializeField] private float flickerTime;
    private float startLightIntensity;
    [SerializeField] private AudioSource audioSource;

    [Header("Flicker Settings")]
    [SerializeField] private Light flashlight;  // The Light component
    public float minIntensity = 0.5f;  // Minimum light intensity
    public float maxIntensity = 2.0f;  // Maximum light intensity
    public float flickerSpeed = 0.1f;  // Time between flickers (in seconds)

    private float flickerTimer;

    private void Update()
    {
        if(isFlickering)
        {
            FlickerLight();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //isFlickering = true;


            Debug.Log("FLICKER");
            PerformEvent();
        }
    }
    public void PerformEvent()
    {
        StartCoroutine(FlickerDelay());
    }

    private void ResetIntensity()
    {
        flashlight.intensity = startLightIntensity;
    }

    IEnumerator FlickerDelay()
    {
        isFlickering = true;
        audioSource.Play();
        yield return new WaitForSeconds(flickerTime);
        isFlickering = false;
        ResetIntensity();
    }

   

    void Start()
    {
        startLightIntensity = flashlight.intensity;
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
