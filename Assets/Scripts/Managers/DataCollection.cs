using UnityEngine;

public class DataCollection : MonoBehaviour
{
    public bool axeRealFound, axeDrawingFound, screwdriverFound, pistolFound, knifeFound, sledgehammerFound;

    public HintEvent[] hintEvents;
    // Total of clues found
    public int clueFound;

    // Clues found by player
    public int clueFoundByPlayer;


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
        // Ensure only one instance of the SceneManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
    }


    public void CollectData()
    {
        clueFoundByPlayer = 0; // Reset to prevent double counting
        foreach (HintEvent item in hintEvents)
        {
            if (item.playerFoundClueByThemselves)
            {
                clueFoundByPlayer++;
            }
        }
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
}
