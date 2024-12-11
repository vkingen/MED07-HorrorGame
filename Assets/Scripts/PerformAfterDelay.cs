using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PerformAfterDelay : MonoBehaviour
{
    [SerializeField] private float delayTime;
    public UnityEvent performAfterDelay;

    public void Perform()
    {
        StartCoroutine(Delay());
        
    }

    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(delayTime);
        performAfterDelay.Invoke();
    }
}
