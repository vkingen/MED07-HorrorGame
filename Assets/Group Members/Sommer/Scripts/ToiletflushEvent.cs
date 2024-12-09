using UnityEngine;

public class ToiletflushEvent : MonoBehaviour
{
    public AudioSource audioSource;

    // Public method to play the toilet flush sound
    public void PlayToiletEvent()
    {
        if (audioSource != null)
            audioSource.Play();
        else
            Debug.LogError("AudioSource component is missing! Please add one to this GameObject.");
    }
}
