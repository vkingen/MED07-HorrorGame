using UnityEngine;
using UnityEngine.Events;

public class PlayerProgressTracker : MonoBehaviour
{
    [HideInInspector] public float totalTimeWithoutClue = 0f;
    private bool trackingTime = false;
    [SerializeField] private float globalTimerMax;
    private float globalTimer;
    public UnityEvent whenGlobalTimerReachesZero;
    [SerializeField] private HintManager hintManager;


    private void Awake()
    {
        globalTimer = globalTimerMax;
    }

    private void Update()
    {
        // DEBUG MODE - DELETE
        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    whenGlobalTimerReachesZero.Invoke();
        //}


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
        hintManager.pauseAlternativeTimer = false;
    }

    public void StopTracking()
    {
        trackingTime = false;
        hintManager.pauseAlternativeTimer = true;
    }

    public void AddToTimer(float timeToAdd)
    {
        totalTimeWithoutClue -= timeToAdd;
        hintManager.ResetAlternativeTimer();
    }

    public void ResetTimer()
    {
        if (totalTimeWithoutClue > 0f)
        {
            totalTimeWithoutClue = 0f;
        }
        hintManager.ResetAlternativeTimer();
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
