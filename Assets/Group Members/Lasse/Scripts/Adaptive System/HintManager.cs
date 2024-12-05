using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance { get; private set; } // Singleton for easy access

    public PlayerProgressTracker playerProgressTracker;
    public Transform player;
    public float hintThreshold = 10f; // Time before triggering a hint

    private List<HintEvent> hintEvents = new List<HintEvent>();
    private string currentRoom = ""; // Tracks the room the player is currently in

   

    // Alternative Settings
    [SerializeField] private float alternativeNotchTimerMax = 30;
    private float alternativeNotchTimerCurrent;
    [HideInInspector] public bool alternativeNotchTimerActive = false;
    [SerializeField] private AudioSource evansAudioSource;
    [SerializeField] private AudioClip[] alternativeNotchClips;
    private bool isPlayingAlternativeNotch = false;
    public bool pauseAlternativeTimer = false;


    public void ActivateAlternativeNotchTimer(bool state)
    {
        alternativeNotchTimerActive = state;
    }
    public void ResetAlternativeTimer()
    {
        alternativeNotchTimerCurrent = alternativeNotchTimerMax;
    }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        ResetAlternativeTimer();
    }

    private void Start()
    {
        hintEvents.AddRange(FindObjectsOfType<HintEvent>());
        playerProgressTracker.StartTracking();
    }

    private void Update()
    {
        CheckForHintTrigger();
        if (alternativeNotchTimerActive && !pauseAlternativeTimer)
        {
            if(alternativeNotchTimerCurrent <= 0)
            {
                alternativeNotchTimerCurrent = alternativeNotchTimerMax;
                ActivateAlternativeNotchTimer(false);
            }
            alternativeNotchTimerCurrent -= Time.deltaTime;
            Debug.Log("State is: " + alternativeNotchTimerActive + " and current time is: " + alternativeNotchTimerCurrent);
        }
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
            else
            {
                if (!isPlayingAlternativeNotch && !alternativeNotchTimerActive)
                {
                    PlayAlternativeNotchVoiceLine();
                    ActivateAlternativeNotchTimer(true);
                    //playerProgressTracker.ResetTimer();
                }
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

    private void PlayAlternativeNotchVoiceLine()
    {
        isPlayingAlternativeNotch = true;
        int randomInt = Random.Range(0, alternativeNotchClips.Length);
        evansAudioSource.clip = alternativeNotchClips[randomInt];
        evansAudioSource.Play();
        StartCoroutine(AlternativeNotchDelay());
    }
    

    IEnumerator AlternativeNotchDelay()
    {
        while (evansAudioSource.isPlaying)
        {
            yield return null; // Wait for the next frame
        }
        isPlayingAlternativeNotch = false;
    }
}
