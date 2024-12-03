using UnityEngine;

public class VoicelineManager : MonoBehaviour
{
    public AudioSource voiceLines;

    // Variable to store the delay
    private float delay = 0f;

    // Set the delay value
    public void SetVoicelineDelay(float newDelay)
    {
        delay = Mathf.Max(0f, newDelay); // Ensure delay is non-negative
    }

    // Play a specific voiceline with the stored delay
    public void PlayVoiceline(AudioClip voiceLineClip)
    {
        if (voiceLines != null && voiceLineClip != null)
        {
            voiceLines.clip = voiceLineClip;

            if (delay > 0)
            {
                voiceLines.PlayDelayed(delay);
            }
            else
            {
                voiceLines.Play();
            }
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned!");
        }
    }
}
