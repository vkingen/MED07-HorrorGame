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
            //Debug.Log($"{gameObject.name}: Stopping draggable displayer");
            draggableDisplayer.SetActive(false);
        }
        else
        {
            //Debug.Log($"{gameObject.name}: Draggable displayer already inactive");
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
            // Only allow the active controller to update the animator
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
            // Only allow the active controller to update the animator
            if (isPulsing)
            {
                activeMurderWeaponAnimatorController = this;
            }
            if (activeMurderWeaponAnimatorController == this)
            {
                murderWeaponAnimator.SetBool("IsPulsing", isPulsing);
            }
        }

        //Debug.Log($"{gameObject.name} pulsing: {isPulsing}");
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
