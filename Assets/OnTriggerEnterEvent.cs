using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private string otherTagToTrigger;
    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(otherTagToTrigger))
        {
            onTriggerEnter.Invoke();
        }
    }
}
