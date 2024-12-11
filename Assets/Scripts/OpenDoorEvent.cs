using UnityEngine;
using HFPS.Systems;

public class OpenDoorEvent : MonoBehaviour
{
    public DynamicObject door;
    public BallRoller ball;

    void Start()
    {
        // Make sure the door reference is assigned in the Inspector
        if (door == null)
        {
            Debug.LogError("Door reference is not assigned!");
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Open the door without player interaction
            door.OpenDoor();
            //ball.PerformEvent();
        }
    }
}
