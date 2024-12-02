using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OfficePhone : MonoBehaviour
{

    // Phone rings              - Audio/Highlight
    // Player clicks on phone   - Audio/New mesh
    // Voice speaks via phone   - Audio
    // Voice stops              - Highlight
    // Player clicks on phone   - Audio / new mesh
    // Main character speaks    - Audio / Highlight on keys
    // Player clicks on keys    - Audio / Fade to black


    [SerializeField] private GameObject phoneOff, phoneOn, carKeys;
    
    [SerializeField] private AudioSource phoneMainAudioSource;
    [SerializeField] private AudioSource phoneDetailsAudioSource;
    [SerializeField] private AudioSource evanTalkingAudioSource;
    [SerializeField] private AudioClip bossVoice, phoneAnswer, phoneHangUp, phoneRingtone, endCall, carDrivingAway;

    
    
    private void Start()
    {
        StartCoroutine(DelayOnStart());
    }
    IEnumerator DelayOnStart()
    {
        yield return new WaitForSeconds(3);
        StartOfficePhoneEvent();
    }

    public void StartOfficePhoneEvent()
    {
        PhoneRings();
    }

    private void PhoneRings()
    {
        Debug.Log("Phone Rings");
        phoneMainAudioSource.loop = true;
        phoneMainAudioSource.clip = phoneRingtone;
        phoneMainAudioSource.Play();

        // Enable interaction
        phoneOff.layer = LayerMask.NameToLayer("Interact");

        // Start highlight effect
        
    }


    public void AnsweringThePhone()
    {
        Debug.Log("Answering the phone");

        // Play phone answering audio
        phoneDetailsAudioSource.clip = phoneAnswer;
        phoneDetailsAudioSource.Play();

        // Play boss voice audio
        phoneMainAudioSource.loop = false;
        phoneMainAudioSource.clip = bossVoice;
        phoneMainAudioSource.Play();

        // Switch phone state
        phoneOff.SetActive(false);
        phoneOn.SetActive(true);

        // Start waiting for the boss talk to complete
        StartCoroutine(WaitForBossTalkToComplete());
    }

    private IEnumerator WaitForBossTalkToComplete()
    {
        // Wait until the boss voice audio finishes playing
        while (phoneMainAudioSource.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        // After the boss talk is complete, execute the next step
        WaitingForPlayerResponseAfterBossTalk();
    }

    public void WaitingForPlayerResponseAfterBossTalk()
    {
        Debug.Log("Waiting For Player Response After Boss Talk");
        phoneDetailsAudioSource.clip = endCall;
        phoneDetailsAudioSource.Play();

        // Enable Interaction
        phoneOn.layer = LayerMask.NameToLayer("Interact");
        
        // Enable Highlight

    }

    public void HangUpThePhone()
    {
        Debug.Log("Hanging up the phone");
        phoneDetailsAudioSource.clip = phoneHangUp;
        phoneDetailsAudioSource.Play();

        phoneOn.SetActive(false);
        phoneOff.SetActive(true);
        phoneOff.layer = LayerMask.NameToLayer("Default");

        evanTalkingAudioSource.Play();

        StartCoroutine(WaitForEvanToFinishTalking());
    }
    private IEnumerator WaitForEvanToFinishTalking()
    {
        // Wait until the boss voice audio finishes playing
        while (evanTalkingAudioSource.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        // After the boss talk is complete, execute the next step
        WaitingForPlayerResponseAfterEvanTalk();
    }

    private void WaitingForPlayerResponseAfterEvanTalk()
    {
        // Enable Interaction
        carKeys.layer = LayerMask.NameToLayer("Interact");
        
        // highlight effect on car keys

    }

    public void PickingUpCarKeys()
    {
        // fade to black
        FadeImage fadeImage = FindObjectOfType<FadeImage>();
        fadeImage.FadeIn();

        // Play car driving away audio
        evanTalkingAudioSource.clip = carDrivingAway;
        evanTalkingAudioSource.Play();

        StartCoroutine(WaitForCar());
    }

    private IEnumerator WaitForCar()
    {
        // Wait until the boss voice audio finishes playing
        while (evanTalkingAudioSource.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        // After the boss talk is complete, execute the next step
        WhenDone();
    }
    public UnityEvent whenDone;

    public void WhenDone()
    {
        whenDone.Invoke();
    }

}
