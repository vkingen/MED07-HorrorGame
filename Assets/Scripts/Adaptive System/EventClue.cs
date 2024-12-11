using UnityEngine;
using UnityEngine.Events;

public class EventClue : MonoBehaviour
{
    PlayerProgressTracker playerProgressTracker;
    //[SerializeField] private bool isClue = false;
    [SerializeField] private float timeToAdd;
    public UnityEvent firstTimeInteract;
    


    private void Awake()
    {
        playerProgressTracker = FindObjectOfType<PlayerProgressTracker>();
    }

    public void ChangeTimer()
    {
        playerProgressTracker.AddToTimer(timeToAdd);
        firstTimeInteract.Invoke();
    }

    public void ResetTimer()
    {
        playerProgressTracker.ResetTimer();
        firstTimeInteract.Invoke();
    }
}
