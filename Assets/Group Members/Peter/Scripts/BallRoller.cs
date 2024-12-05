using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BallRoller : MonoBehaviour
{
    public float rollForce = 0.01f; // Force applied to roll the ball forward
    private Rigidbody rb;
    public AudioSource audioSource;
    //public Material dissolveMaterial;
    [SerializeField] private float waitRoll = 1;
    [SerializeField] public AudioClip ballRollingSound;
    [SerializeField] private bool isTriggered = false;
    [SerializeField] private DissolveController dissolveController;

    public UnityEvent whenPerformed;
    //private Renderer renderer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //renderer = GetComponent<Renderer>();
    }
    public void PerformEvent()
    {
        StartCoroutine(RollAndStop());
        StartCoroutine(PlayRollingSound());
        whenPerformed.Invoke();
    }
    private IEnumerator RollAndStop()
    {
        if (!isTriggered)
        {
            yield return new WaitForSeconds(waitRoll);
            // Apply force to roll the ball forward
            rb.AddForce(transform.forward * rollForce, ForceMode.Impulse);

            // Wait for 4 seconds
            yield return new WaitForSeconds(4.2f);

            // Stop the ball
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isTriggered = true;

            //AddMaterialAtTop();
            dissolveController.enabled = true;
        }
    }

    private IEnumerator PlayRollingSound()
    {
        if (!isTriggered)
        {
            /*if (ballRollingSound == null)
        {
            Debug.LogWarning("No ball rolling sound assigned to BallRoller event.");
            yield return null;
        }*/
            yield return new WaitForSeconds(1f);
            // Play the sound
            audioSource.clip = ballRollingSound;
            audioSource.Play();
            yield return null;
        }
    }

    /*void AddMaterialAtTop()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogWarning("No MeshRenderer found on the object!");
            return;
        }

        if (dissolveMaterial == null)
        {
            Debug.LogWarning("No material assigned to add!");
            return;
        }

        Material[] currentMaterials = meshRenderer.materials;

        // Create a new array with one extra slot
        Material[] updatedMaterials = new Material[currentMaterials.Length + 1];

        // Add the new material at the first position
        updatedMaterials[0] = dissolveMaterial;

        // Copy the existing materials to the new array
        for (int i = 0; i < currentMaterials.Length; i++)
        {
            updatedMaterials[i + 1] = currentMaterials[i];
        }

        // Assign the updated array back to the renderer
        meshRenderer.materials = updatedMaterials;

        Debug.Log("New material added at the top index!");
    }*/
}
