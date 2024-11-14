using UnityEngine;

public class PlayerProgressTracker : MonoBehaviour
{
    [HideInInspector] public float totalTimeWithoutClue = 0f;
    private bool trackingTime = false;

    private void Update()
    {
        if (trackingTime)
        {
            totalTimeWithoutClue += Time.deltaTime;
        }
    }

    public void StartTracking()
    {
        trackingTime = true;
    }

    public void StopTracking()
    {
        trackingTime = false;
    }

    public void ResetTimer()
    {
        totalTimeWithoutClue = 0f;
    }

    public float GetTotalTimeWithoutClue()
    {
        return totalTimeWithoutClue;
    }
}
