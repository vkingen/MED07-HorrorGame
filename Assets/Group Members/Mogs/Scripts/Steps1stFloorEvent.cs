using UnityEngine;
using System.Collections;

public class Steps1stFloorEvent : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Assign the AudioClip in the Inspector
    [SerializeField] private AudioClip stepSound;

    // Speed at which the audio fades out (volume reduction per second)
    [SerializeField] private float fadeOutSpeed = 1.0f;

    // Reference to the trigger GameObject (Upstairs Corridor)
    [SerializeField] private GameObject fadeOutTrigger;

    // Reference to the player GameObject
    [SerializeField] private GameObject player;

    private void Awake()
    {
        // Ensure an AudioSource is attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PerformEvent()
    {
        PlayStepSound();
    }

    private void PlayStepSound()
    {
        if (stepSound == null)
        {
            Debug.LogWarning("No step sound assigned to Steps1stFloorEvent.");
            return;
        }

        // Play the sound
        audioSource.clip = stepSound;
        audioSource.volume = 1.0f; // Ensure full volume at the start
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the fade-out trigger
        if (fadeOutTrigger != null && other.gameObject == fadeOutTrigger && player != null)
        {
            // Verify the collision involves the player
            if (player.GetComponent<Collider>() != null && other.gameObject == player)
            {
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
