using TMPro;
using UnityEngine;

public class DetectiveSelectionSystem : MonoBehaviour
{
    // Keep track of all the dropzones and wether or not they are Occupied or not. 
    // If both dropzones are Occupied then set submitButton active=true else set submitButton active=false

    // when pressing the submitButton then keep track of what dropzones has the correct placed draggableObjects

    public GameObject submitButton; // Reference to the submit button

    [SerializeField] private DropZone[] dropZones; // Array to hold references to all DropZones in the scene

    [SerializeField] private FPSRaycastUIButton raycaster;

    [SerializeField] private TMP_Text resultsText;

    [SerializeField] private FadeImage fade;

    private void Update()
    {
        // Check if all DropZones are occupied
        bool allOccupied = true;
        foreach (DropZone dropZone in dropZones)
        {
            if (!dropZone.IsOccupied)
            {
                allOccupied = false;
                break;
            }
        }

        // Enable or disable the submit button based on whether all DropZones are occupied
        raycaster.isActivated = allOccupied;
        submitButton.SetActive(allOccupied);
    }

    public void OnSubmitButtonPressed()
    {
        int correctCount = 0;

        // Iterate through all DropZones to count how many have the correct object placed
        foreach (DropZone dropZone in dropZones)
        {
            if (dropZone.IsCorrectObjectPlaced)
            {
                correctCount++;
            }
        }

        // Log or handle the results
        resultsText.text = ($"Correct answers: {correctCount} / {dropZones.Length}");
        //Debug.Log($"Correctly placed objects: {correctCount}/{dropZones.Length}");
        fade.FadeIn();
        // Add any additional logic for what happens after submission
        // e.g., trigger a win condition, display feedback, etc.
    }
}
