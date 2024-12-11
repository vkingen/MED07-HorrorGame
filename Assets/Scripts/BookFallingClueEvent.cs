using System.Collections;
using UnityEngine;

public class BookFallingClueEvent : MonoBehaviour
{
    [SerializeField] private GameObject book;
    AudioSource audioSource;
    Rigidbody rigidbody;
    public float multiplier;
    bool isEventActivated = false;
    bool hasPlayedAudio = false;

    private void Awake()
    {
        audioSource = book.GetComponent<AudioSource>();
        rigidbody = book.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(isEventActivated)
            CheckForBookVelocity();
    }


    public void PerformEvent()
    {
        StartCoroutine(Delay());
        // audioSource.Play();

        // Highlight object with shader

        // Move book
        rigidbody.AddForce(transform.right * multiplier);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        isEventActivated = true;
    }

    private void CheckForBookVelocity()
    {
        if(rigidbody.linearVelocity.magnitude <= 0.1)
        {
            if(!hasPlayedAudio)
            {
                audioSource.Play();
                hasPlayedAudio = true;
            }
        }
    }
}
