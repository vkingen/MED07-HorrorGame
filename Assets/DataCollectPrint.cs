using UnityEngine;

public class DataCollectPrint : MonoBehaviour
{
    public DropZone dropZoneMurder, dropZoneWeapon;
    public void GetDataCollectionReference()
    {
        
        DataCollection dataCollection = FindObjectOfType<DataCollection>();

        dataCollection.murderSelected = dropZoneMurder.occupyingObject.name;
        dataCollection.murderWeaponSelected = dropZoneWeapon.occupyingObject.name;

        dataCollection.ExportToCSV();
    } 
}
