using UnityEngine;
using UnityEngine.Events;

public class PlayerProgressTracker : MonoBehaviour
{
    [HideInInspector] public float totalTimeWithoutClue = 0f;
    private bool trackingTime = false;
    [SerializeField] private float globalTimerMax;
    private float globalTimer;
    public UnityEvent whenGlobalTimerReachesZero;

    private void Awake()
    {
        globalTimer = globalTimerMax;
    }

    private void Update()
    {
        globalTimer -= Time.deltaTime;
        if(globalTimer <= 0f)
        {
            whenGlobalTimerReachesZero.Invoke();
        }
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

    public void AddToTimer(float timeToAdd)
    {
        totalTimeWithoutClue -= timeToAdd;
    }

    public void ResetTimer()
    {
        if(totalTimeWithoutClue > 0f)
        {
            totalTimeWithoutClue = 0f;
        }
    }

    public float GetTotalTimeWithoutClue()
    {
        return totalTimeWithoutClue;
    }

    public void ChangeScene(int sceneIndex)
    {
        PersistentSceneManager.Instance.LoadScene(sceneIndex);
    }
}
