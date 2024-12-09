using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DraggableObjectNew : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging;
    private DropZone currentDropZone;
    private Vector3 originalPosition;

    [SerializeField] private string descriptionText;
    [SerializeField] private GameObject draggableDisplayer;
    [SerializeField] private Image draggableDisplayerImage;
    [SerializeField] private Sprite imageToDisplay;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Animator murdererAnimator;
    [SerializeField] private Animator murderWeaponAnimator;

    // Define the type of this draggable object
    public ItemTypeNew objectType;

    // Name of the weapon or clue related to this draggable object
    [SerializeField] private string relatedWeaponName;

    // Boundary within which the object can be dragged
    public BoxCollider dragBoundary;

    // Speed at which the object follows the mouse position (lower values create more delay)
    public float followSpeed = 10f;

    // Duration for smooth return to original position
    public float returnSpeed = 5f;

    private bool isHovering = false;

    // Static reference to track the current UI controller
    private static DraggableObjectNew currentUIController;

    private void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position; // Save original position in case we need to reset

        if (objectType == ItemTypeNew.MurderWeapon)
        {
            // Check if the related weapon is found
            if (DataCollection.Instance != null)
            {
                bool isWeaponFound = CheckWeaponStatus();
                if (!isWeaponFound)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    private bool CheckWeaponStatus()
    {
        // Log the current status of the weapons
        Debug.Log($"Checking status for {relatedWeaponName}");

        // Custom logic to handle the axe case
        if (relatedWeaponName == "axeReal" || relatedWeaponName == "axeDrawing")
        {
            // If both axeRealFound and axeDrawingFound are true, keep axeRealFound and destroy axeDrawing
            if (DataCollection.Instance.axeRealFound && DataCollection.Instance.axeDrawingFound)
            {
                Debug.Log("Both axeRealFound and axeDrawingFound are true. Keeping axeRealFound and destroying axeDrawing.");
                // Destroy the axeDrawing object
                DestroyGameObject("axeDrawing");
                return true; // Return true because axeRealFound should stay
            }
        }

        // Continue with regular weapon checks
        switch (relatedWeaponName)
        {
            case "axeReal":
                return DataCollection.Instance.axeRealFound;
            case "axeDrawing":
                return DataCollection.Instance.axeDrawingFound;
            case "screwdriver":
                return DataCollection.Instance.screwdriverFound;
            case "pistol":
                return DataCollection.Instance.pistolFound;
            case "knife":
                return DataCollection.Instance.knifeFound;
            case "sledgehammer":
                return DataCollection.Instance.sledgehammerFound;
            default:
                Debug.LogWarning("Unknown weapon name: " + relatedWeaponName);
                return false;
        }
    }

    private void DestroyGameObject(string weaponName)
    {
        // Iterate through all the draggable objects and destroy the one with the matching name
        DraggableObjectNew[] draggableObjects = FindObjectsOfType<DraggableObjectNew>();
        foreach (var draggable in draggableObjects)
        {
            if (draggable.relatedWeaponName == weaponName)
            {
                Debug.Log($"Destroying object: {draggable.gameObject.name} because {weaponName} is already found");
                Destroy(draggable.gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        StopAllCoroutines(); // Stop any ongoing movement back to original position
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;

        if (currentDropZone != null)
        {
            currentDropZone.ClearOccupyingObject();
            currentDropZone = null;
        }
    }

    private void DisplayDraggableDisplayer()
    {
        if (currentUIController == this)
        {
            draggableDisplayerImage.sprite = imageToDisplay;
            textField.text = descriptionText;
            draggableDisplayer.SetActive(true);
        }
    }

    private void StopDisplayDraggableDisplayer()
    {
        if (draggableDisplayer != null && draggableDisplayer.activeSelf)
        {
            draggableDisplayer.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        isHovering = true;

        // Grant ownership if no one else has it
        if (currentUIController == null || currentUIController == this)
        {
            currentUIController = this;
            DisplayDraggableDisplayer();
        }
    }

    private void OnMouseExit()
    {
        isHovering = false;

        if (currentUIController == this && !isDragging)
        {
            StopDisplayDraggableDisplayer();
            currentUIController = null;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (currentUIController == this)
        {
            StopDisplayDraggableDisplayer();
            currentUIController = null;
        }

        if (currentDropZone != null && !currentDropZone.IsOccupied && currentDropZone.CanAcceptObject(this))
        {
            transform.position = currentDropZone.transform.position;
            transform.rotation = currentDropZone.transform.rotation;
            currentDropZone.SetOccupyingObject(this);
        }
        else
        {
            StartCoroutine(SmoothReturnToOriginalPosition());
        }
    }

    // Static references to track which object is controlling each animator
    private static DraggableObjectNew activeMurderAnimatorController;
    private static DraggableObjectNew activeMurderWeaponAnimatorController;

    private void HandleAnimatorState(bool isPulsing)
    {
        // Determine the correct static controller for this object's type
        if (objectType == ItemTypeNew.Murder && murdererAnimator != null)
        {
            if (isPulsing)
            {
                activeMurderAnimatorController = this;
            }
            if (activeMurderAnimatorController == this)
            {
                murdererAnimator.SetBool("IsPulsing", isPulsing);
            }
        }
        else if (objectType == ItemTypeNew.MurderWeapon && murderWeaponAnimator != null)
        {
            if (isPulsing)
            {
                activeMurderWeaponAnimatorController = this;
            }
            if (activeMurderWeaponAnimatorController == this)
            {
                murderWeaponAnimator.SetBool("IsPulsing", isPulsing);
            }
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            HandleAnimatorState(true); // Enable animation for the current object

            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            if (dragBoundary != null)
            {
                targetPosition = ClampPositionToBoundary(targetPosition, dragBoundary);
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            if (currentUIController == this)
            {
                DisplayDraggableDisplayer();
            }
        }
        else if (isHovering)
        {
            HandleAnimatorState(true); // Enable animation during hover

            if (currentUIController == null || currentUIController == this)
            {
                currentUIController = this;
                DisplayDraggableDisplayer();
            }
        }
        else
        {
            HandleAnimatorState(false); // Stop animation when neither dragging nor hovering

            if (currentUIController == this)
            {
                StopDisplayDraggableDisplayer();
                currentUIController = null; // Release ownership
            }
        }

        // Clear static ownership if no longer dragging or hovering
        if (!isDragging && !isHovering)
        {
            if (activeMurderAnimatorController == this)
            {
                activeMurderAnimatorController = null;
            }
            if (activeMurderWeaponAnimatorController == this)
            {
                activeMurderWeaponAnimatorController = null;
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private Vector3 ClampPositionToBoundary(Vector3 position, BoxCollider boundary)
    {
        Vector3 minBounds = boundary.bounds.min;
        Vector3 maxBounds = boundary.bounds.max;

        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);
        position.z = Mathf.Clamp(position.z, minBounds.z, maxBounds.z);

        return position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropZone"))
        {
            DropZone dropZone = other.GetComponent<DropZone>();

            if (dropZone != null && !dropZone.IsOccupied && dropZone.CanAcceptObject(this))
            {
                currentDropZone = dropZone;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropZone") && currentDropZone == other.GetComponent<DropZone>())
        {
            currentDropZone = null;
        }
    }

    private void OnDisable()
    {
        if (currentDropZone != null)
        {
            currentDropZone.ClearOccupyingObject();
        }

        // Revoke UI ownership if this object is disabled
        if (currentUIController == this)
        {
            currentUIController = null;
        }
    }

    private IEnumerator SmoothReturnToOriginalPosition()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, originalPosition, elapsedTime);
            elapsedTime += Time.deltaTime * returnSpeed;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
