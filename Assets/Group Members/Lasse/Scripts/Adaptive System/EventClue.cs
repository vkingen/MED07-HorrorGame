using UnityEngine;

public class EventClue : MonoBehaviour
{
    PlayerProgressTracker playerProgressTracker;
    //[SerializeField] private bool isClue = false;
    [SerializeField] private float timeToAdd;
    private void Awake()
    {
        playerProgressTracker = FindObjectOfType<PlayerProgressTracker>();
    }

    public void ChangeTimer()
    {
        playerProgressTracker.AddToTimer(timeToAdd);
    }

    public void ResetTimer()
    {
        playerProgressTracker.ResetTimer();
    }
}
