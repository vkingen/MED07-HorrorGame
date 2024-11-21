using UnityEngine;
using System.Collections;

public class BallRoller : MonoBehaviour
{
    public float rollForce = 0.01f; // Force applied to roll the ball forward
    private Rigidbody rb;
    public AudioSource audioSource;
    [SerializeField] public AudioClip ballRollingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RollAndStop());
        StartCoroutine(PlayRollingSound());
    }
    public void PerformEvent()
    {
        
    }
    private IEnumerator RollAndStop()
    {  
        // Apply force to roll the ball forward
        rb.AddForce(transform.forward * rollForce, ForceMode.Impulse);

        // Wait for 4 seconds
        yield return new WaitForSeconds(3.5f);

        // Stop the ball
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private IEnumerator PlayRollingSound()
    {
        /*if (ballRollingSound == null)
        {
            Debug.LogWarning("No ball rolling sound assigned to BallRoller event.");
            yield return null;
        }*/

        // Play the sound
        audioSource.clip = ballRollingSound;
        audioSource.volume = 1.0f; // Ensure full volume at the start
        audioSource.Play();
        yield return null;
    }
}
