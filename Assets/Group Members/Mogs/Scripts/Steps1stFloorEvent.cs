using UnityEngine;

public class Steps1stFloorEvent : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Assign the AudioClip in the Inspector
    [SerializeField] private AudioClip stepSound;

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
}
