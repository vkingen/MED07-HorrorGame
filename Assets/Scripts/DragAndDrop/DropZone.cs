using System.Linq;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    // Define the type of item this drop zone accepts
    public ItemTypeNew zoneType;

    // Track if this DropZone is occupied
    public DraggableObjectNew occupyingObject;

    // Define the correct name of the draggable object for this DropZone
    public string[] correctDraggableObjectName;

    // Track if the correct object is placed in the DropZone
    public bool IsCorrectObjectPlaced =>
        occupyingObject != null &&
        correctDraggableObjectName.Contains(occupyingObject.name);

    // Property to check if the DropZone is occupied
    public bool IsOccupied => occupyingObject != null;

    // Method to assign the occupying object
    public void SetOccupyingObject(DraggableObjectNew obj)
    {
        occupyingObject = obj;
        Debug.Log($"{gameObject.name}: Occupied by {obj.name}");
        Debug.Log($"{gameObject.name}: IsCorrectObjectPlaced = {IsCorrectObjectPlaced}");
    }

    // Method to clear the occupying object
    public void ClearOccupyingObject()
    {
        Debug.Log($"{gameObject.name}: Cleared occupation");
        occupyingObject = null;
    }

    // Method to check if a DraggableObject's type matches this DropZone's type
    public bool CanAcceptObject(DraggableObjectNew obj)
    {
        return obj.objectType == zoneType;
    }
}
