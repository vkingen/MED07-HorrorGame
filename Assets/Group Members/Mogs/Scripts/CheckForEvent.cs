using UnityEngine;
using System.Collections;

public class CheckForEvent : MonoBehaviour
{
    [SerializeField] private HintEvent hintEvent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeOutSpeed;

    // This is to ensure the object has a collider set to trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player (you can modify this based on your player object)
        if (other.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            if (hintEvent != null && hintEvent.isTriggered && audioSource.isPlaying) // Only trigger if the audio is playing
            {
                Debug.Log("The HintEvent has been triggered and player collided, audio is playing.");
                StartCoroutine(FadeOutSound());
            }
        }
    }

    private IEnumerator FadeOutSound()
    {
        // Gradually decrease the volume to 0
        while (audioSource.volume > 0)
        {
            audioSource.volume -= fadeOutSpeed * Time.deltaTime;
            yield return null;
        }

        // Stop the audio completely after fading out
        audioSource.Stop();
        audioSource.volume = 1.0f; // Reset volume for future use
    }
}
