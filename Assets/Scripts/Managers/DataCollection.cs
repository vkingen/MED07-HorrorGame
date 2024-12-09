using UnityEngine;

public class DataCollection : MonoBehaviour
{
    public bool axeRealFound, axeDrawingFound, screwdriverFound, pistolFound, knifeFound, sledgehammerFound;

    private HintEvent[] hintEvents; // Keep this private
    // Total of clues found
    public int clueTriggered;

    // Clues found by player
    public int clueFoundByPlayer;
    public int alternativeNotchCount;

    private static DataCollection instance;



    public static DataCollection Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of the DataCollection exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes

        // Find all HintEvent objects in the scene and store them in the hintEvents array
        hintEvents = FindObjectsOfType<HintEvent>();
    }

    public void AlternativeNotchPlayed()
    {
        alternativeNotchCount++;
    }

    public void FindingWeapon(string name)
    {
        switch (name)
        {
            case "axeReal":
                axeRealFound = true;
                break;
            case "axeDrawing":
                axeDrawingFound = true;
                break;
            case "screwdriver":
                screwdriverFound = true;
                break;
            case "pistol":
                pistolFound = true;
                break;
            case "knife":
                knifeFound = true;
                break;
            case "sledgehammer":
                sledgehammerFound = true;
                break;
            default:
                Debug.LogWarning("Weapon name not recognized: " + name);
                break;
        }
    }

    // ---------------------------------- End Game Data Collection --------------------------------

    public void CollectData()
    {
        CollectCluesFoundByPlayer();
    }

    public void CollectCluesFoundByPlayer()
    {
        clueFoundByPlayer = 0; // Reset to prevent double counting
        clueTriggered = 0; // Reset to prevent double counting
        foreach (HintEvent item in hintEvents)
        {
            if(item.isTriggered)
            {
                clueTriggered++;
            }
            if (item.playerFoundClueByThemselves)
            {
                clueFoundByPlayer++;
            }
        }
    }
}
