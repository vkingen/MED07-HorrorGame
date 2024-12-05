using UnityEngine;
using System.Collections;

public class DraggableObjectNew : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging;
    private DropZone currentDropZone;
    private Vector3 originalPosition;

    [SerializeField] private GameObject descriptionText;

    // Define the type of this draggable object
    public ItemTypeNew objectType;

    // Boundary within which the object can be dragged
    public BoxCollider dragBoundary;

    // Speed at which the object follows the mouse position (lower values create more delay)
    public float followSpeed = 10f;

    // Duration for smooth return to original position
    public float returnSpeed = 5f;  // Adjust this value to control return speed

    private void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position; // Save original position in case we need to reset
    }

    private void OnMouseDown()
    {
        StopAllCoroutines();  // Stop any ongoing movement back to original position
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;

        if (currentDropZone != null)
        {
            currentDropZone.ClearOccupyingObject();
            currentDropZone = null;
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            if (dragBoundary != null)
            {
                targetPosition = ClampPositionToBoundary(targetPosition, dragBoundary);
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            if(descriptionText!= null)
                descriptionText.SetActive(true);
        }
        else
        {
            if (descriptionText != null)
                descriptionText.SetActive(false);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (currentDropZone != null && !currentDropZone.IsOccupied && currentDropZone.CanAcceptObject(this))
        {
            transform.position = currentDropZone.transform.position;
            transform.rotation = currentDropZone.transform.rotation;
            currentDropZone.SetOccupyingObject(this);
        }
        else
        {
            // Start smooth return to original position
            StartCoroutine(SmoothReturnToOriginalPosition());
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
    }

    // Coroutine to smoothly return to original position
    private IEnumerator SmoothReturnToOriginalPosition()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        // Smoothly move back to the original position over time
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, originalPosition, elapsedTime);
            elapsedTime += Time.deltaTime * returnSpeed;  // Increase elapsed time based on speed
            yield return null;
        }

        // Ensure exact position at the end
        transform.position = originalPosition;
    }
}
