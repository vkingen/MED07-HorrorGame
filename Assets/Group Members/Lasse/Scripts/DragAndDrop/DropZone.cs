using UnityEngine;

public class DropZone : MonoBehaviour
{
    // Define the type of item this drop zone accepts
    public ItemTypeNew zoneType;

    // Track if this DropZone is occupied
    private DraggableObjectNew occupyingObject;

    public bool IsOccupied => occupyingObject != null;

    public void SetOccupyingObject(DraggableObjectNew obj)
    {
        occupyingObject = obj;
    }

    public void ClearOccupyingObject()
    {
        occupyingObject = null;
    }

    // Check if a DraggableObject's type matches this DropZone's type
    public bool CanAcceptObject(DraggableObjectNew obj)
    {
        return obj.objectType == zoneType;
    }
}
