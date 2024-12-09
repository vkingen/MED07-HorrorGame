using UnityEngine;
using UnityEngine.Events;

public class HintEvent : MonoBehaviour
{
    public string roomName;
    public bool isTriggered = false; // Flag to avoid retriggering
    public UnityEvent eventToTrigger;
    [HideInInspector] public bool playerFoundClueByThemselves; 

    public void DeactiveEvent()
    {
        if(!isTriggered)
        {
            playerFoundClueByThemselves = true;
            isTriggered = true;
        }
    }

    public void TriggerEvent()
    {
        if (isTriggered) return;

        isTriggered = true;
        Debug.Log($"Hint Event Triggered: {transform.name}");

        PerformHintEffect();
    }

    private void PerformHintEffect()
    {
        // Implement your hint effect here, e.g., playing a sound or moving an object
        eventToTrigger.Invoke();
    }

    // Optional: reset for reusability (if needed)
    public void ResetEvent()
    {
        isTriggered = false;
    }
}
