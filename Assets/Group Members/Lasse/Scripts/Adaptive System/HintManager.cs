using UnityEngine;
using System.Collections.Generic;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance { get; private set; } // Singleton for easy access

    public PlayerProgressTracker playerProgressTracker;
    public Transform player;
    public float hintThreshold = 10f; // Time before triggering a hint

    private List<HintEvent> hintEvents = new List<HintEvent>();
    private string currentRoom = ""; // Tracks the room the player is currently in

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        hintEvents.AddRange(FindObjectsOfType<HintEvent>());
        playerProgressTracker.StartTracking();
    }

    private void Update()
    {
        CheckForHintTrigger();
    }

    public void SetCurrentRoom(string roomName)
    {
        currentRoom = roomName;
        Debug.Log($"Player is now in room: {currentRoom}");
    }

    private void CheckForHintTrigger()
    {
        // Check if total time without clue exceeds threshold
        if (playerProgressTracker.GetTotalTimeWithoutClue() >= hintThreshold)
        {
            HintEvent closestEvent = FindClosestHintEventInRoom();
            if (closestEvent != null)
            {
                closestEvent.TriggerEvent();
                playerProgressTracker.ResetTimer(); // Reset the timer since a hint was given
            }
        }
    }

    private HintEvent FindClosestHintEventInRoom()
    {
        HintEvent closestEvent = null;
        float closestDistance = Mathf.Infinity;

        foreach (HintEvent hintEvent in hintEvents)
        {
            if (!hintEvent.isTriggered && hintEvent.roomName == currentRoom)
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
