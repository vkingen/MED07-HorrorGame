using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public string roomName; // Unique name for the room

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HintManager.Instance.SetCurrentRoom(roomName); // Update the current room in the HintManager
        }
    }
}
