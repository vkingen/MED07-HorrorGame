using UnityEngine;
using UnityEngine.UI;

public class FPSRaycastUIButton : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera; // The FPS camera
    [SerializeField] private float interactionRange = 10f; // Maximum interaction distance
    [SerializeField] private Image cursorImage; // Optional: Visual feedback for the cursor
    public bool isActivated = false;

    private void Update()
    {
        if (!isActivated) return;
        // Raycast in the forward direction of the camera
        Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits anything within range
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            //Debug.Log(hit.transform.name);
            // Check if the hit object has a Button component
            Button button = hit.transform.GetComponentInChildren<Button>();
            if (button != null)
            {
                // Optional: Change cursor appearance to indicate interaction
                if (cursorImage != null)
                    cursorImage.color = button.colors.pressedColor;

                // Check for interaction input
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    button.onClick.Invoke(); // Trigger the button's click event
                }
                return; // Exit if a button is hit to avoid resetting cursor color
            }
        }

        // Reset cursor appearance if not hovering over a button
        if (cursorImage != null)
            cursorImage.color = Color.white;
    }
}
