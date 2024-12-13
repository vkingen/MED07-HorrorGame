using HFPS.Systems;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCollection : MonoBehaviour
{
    [SerializeField] private bool testing;

    public bool axeRealFound, axeDrawingFound, screwdriverFound, pistolFound, knifeFound, sledgehammerFound;

    private HintEvent[] hintEvents;
    public int clueTriggered;
    public int clueFoundByPlayer;
    public int alternativeNotchCount;

    private static DataCollection instance;
    private Dictionary<string, float> timeSpentInAreas = new Dictionary<string, float>();

    public string murderSelected, murderWeaponSelected;

    public static DataCollection Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

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
            case "axeReal": axeRealFound = true; break;
            case "axeDrawing": axeDrawingFound = true; break;
            case "screwdriver": screwdriverFound = true; break;
            case "pistol": pistolFound = true; break;
            case "knife": knifeFound = true; break;
            case "sledgehammer": sledgehammerFound = true; break;
            default: Debug.LogWarning("Weapon name not recognized: " + name); break;
        }
    }

    public void CollectData()
    {
        CollectCluesFoundByPlayer();
        CollectRoomTimes();
    }

    public void CollectCluesFoundByPlayer()
    {
        clueFoundByPlayer = 0;
        clueTriggered = 0;
        foreach (HintEvent item in hintEvents)
        {
            if (item.isTriggered) clueTriggered++;
            if (item.playerFoundClueByThemselves) clueFoundByPlayer++;
        }
    }

    private void CollectRoomTimes()
    {
        TimeSpentInRoom[] rooms = FindObjectsOfType<TimeSpentInRoom>();

        foreach (var room in rooms)
        {
            string objectName = room.roomName;
            if (!timeSpentInAreas.ContainsKey(objectName))
            {
                timeSpentInAreas.Add(objectName, 0f);
            }

            TimeSpentInRoom zone = room.GetComponent<TimeSpentInRoom>();
            if (zone != null)
            {
                timeSpentInAreas[objectName] += zone.timeSpentInZone;
            }
        }
    }

    public void ExportToCSV()
    {
        if (testing) return;

        string filePath = Path.Combine(Application.dataPath, "GameData_001.csv");

        // Check if the file already exists, and if so, increment the filename
        int fileIndex = 1;
        while (File.Exists(filePath))
        {
            // Generate new file name with iteration (e.g., GameData_001.csv, GameData_002.csv, etc.)
            filePath = Path.Combine(Application.dataPath, $"GameData_{fileIndex:D3}.csv");
            fileIndex++;
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header row
            writer.WriteLine("Category,Value");

            // Write weapons found
            writer.WriteLine("Axe Real Found," + axeRealFound);
            writer.WriteLine("Axe Drawing Found," + axeDrawingFound);
            writer.WriteLine("Screwdriver Found," + screwdriverFound);
            writer.WriteLine("Pistol Found," + pistolFound);
            writer.WriteLine("Knife Found," + knifeFound);
            writer.WriteLine("Sledgehammer Found," + sledgehammerFound);

            // Write clue data
            writer.WriteLine("Clues Triggered," + clueTriggered);
            writer.WriteLine("Clues Found by Player," + clueFoundByPlayer);

            // Write alternative notch count
            writer.WriteLine("Alternative Notches Played," + alternativeNotchCount);

            // Write end results
            writer.WriteLine("Murder Selected," + (string.IsNullOrEmpty(murderSelected) ? "None" : murderSelected));
            writer.WriteLine("Murder Weapon Selected," + (string.IsNullOrEmpty(murderWeaponSelected) ? "None" : murderWeaponSelected));

            // Write room times (rounded to integers without decimals)
            foreach (var entry in timeSpentInAreas)
            {
                int roundedTime = Mathf.RoundToInt(entry.Value); // Round the time spent to the nearest whole number
                writer.WriteLine($"Time Spent in {entry.Key}," + roundedTime);
            }
        }
        Debug.Log($"Data exported to {filePath}");
    }
}
