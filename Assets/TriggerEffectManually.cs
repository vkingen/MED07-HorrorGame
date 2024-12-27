using UnityEngine;
using UnityEngine.Events;

public class TriggerEffectManually : MonoBehaviour
{
    private bool isInTrigger = false;

    public UnityEvent trigger;


    private void Update()
    {
        if(isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.T)) 
            {
                trigger.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}
