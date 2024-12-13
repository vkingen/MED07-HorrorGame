using UnityEngine;

public class TimeSpentInRoom : MonoBehaviour
{
    RoomTrigger roomTrigger;
    [HideInInspector] public string roomName;

    public float timeSpentInZone; 
    private bool isCounting = false;
    private void Awake()
    {
        roomTrigger = GetComponent<RoomTrigger>();
        roomName = roomTrigger.roomName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCounting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCounting = false;
        }
    }

    private void Update()
    {
        if (isCounting)
        {
            timeSpentInZone += Time.deltaTime;
        }
    }
}
