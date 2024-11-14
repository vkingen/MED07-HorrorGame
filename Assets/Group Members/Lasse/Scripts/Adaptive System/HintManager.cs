using UnityEngine;
using System.Collections.Generic;

public class HintManager : MonoBehaviour
{
    public PlayerProgressTracker playerProgressTracker;
    public Transform player;
    public float hintThreshold = 10f; // Time before triggering a hint
    public float hintCooldown = 10f; // Cooldown between hints

    private float hintCooldownTimer = 0f;
    private List<HintEvent> hintEvents = new List<HintEvent>();

    private void Start()
    {
        hintEvents.AddRange(FindObjectsOfType<HintEvent>());
        playerProgressTracker.StartTracking();
    }

    private void Update()
    {
        // Update cooldown timer
        if (hintCooldownTimer > 0f)
        {
            hintCooldownTimer -= Time.deltaTime;
        }
        else
        {
            CheckForHintTrigger();
        }
    }

    private void CheckForHintTrigger()
    {
        // Check if total time without clue exceeds threshold
        if (playerProgressTracker.GetTotalTimeWithoutClue() >= hintThreshold)
        {
            HintEvent closestEvent = FindClosestHintEvent();
            if (closestEvent != null)
            {
                closestEvent.TriggerEvent();
                playerProgressTracker.ResetTimer(); // Reset the timer since a hint was given
                hintCooldownTimer = hintCooldown; // Start cooldown
            }
        }
    }

    private HintEvent FindClosestHintEvent()
    {
        HintEvent closestEvent = null;
        float closestDistance = Mathf.Infinity;

        foreach (HintEvent hintEvent in hintEvents)
        {
            if (!hintEvent.isTriggered)
            {
                float distance = Vector3.Distance(player.position, hintEvent.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEvent = hintEvent;
                }
            }
        }

        return closestEvent;
    }
}
