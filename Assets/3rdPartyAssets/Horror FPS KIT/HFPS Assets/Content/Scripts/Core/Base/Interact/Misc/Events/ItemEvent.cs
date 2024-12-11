using UnityEngine;
using UnityEngine.Events;

namespace HFPS.Systems
{
    public class ItemEvent : MonoBehaviour, IItemEvent
    {
        public UnityEvent InteractEvent;
        public UnityEvent onExaminationCancelled;

        [SaveableField, HideInInspector]
        public bool eventExecuted;
        [SaveableField, HideInInspector]
        public bool cancelEventExecuted;

        public void OnItemEvent()
        {
            if (!eventExecuted)
            {
                InteractEvent?.Invoke();
            }
        }

        public void OnExamineCancelled() 
        {
            if (!cancelEventExecuted)
            {
                onExaminationCancelled?.Invoke();
            }
        }
    }
}