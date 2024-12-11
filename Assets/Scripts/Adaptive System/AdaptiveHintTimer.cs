using UnityEngine;
using UnityEngine.UI;

public class AdaptiveHintTimer : MonoBehaviour
{
    Text hintTimerText;
    PlayerProgressTracker tracker;
    HintManager hintManager;

    private void Awake()
    {
        hintTimerText = GetComponent<Text>();
        tracker = FindObjectOfType<PlayerProgressTracker>();
        hintManager = FindObjectOfType<HintManager>();
    }

    private void Update()
    {
        hintTimerText.text = "Hint time progression: " + tracker.totalTimeWithoutClue.ToString("F1") + " / " + hintManager.hintThreshold.ToString(); ;
    }


}
