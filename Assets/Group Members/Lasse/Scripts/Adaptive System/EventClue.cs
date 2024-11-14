using UnityEngine;

public class EventClue : MonoBehaviour
{
    PlayerProgressTracker playerProgressTracker;
    private void Awake()
    {
        playerProgressTracker = FindObjectOfType<PlayerProgressTracker>();
    }

    public void ResetHintTimer()
    {
        playerProgressTracker.ResetTimer();
    }
}
