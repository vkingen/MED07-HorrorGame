using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OfficePhone : MonoBehaviour
{
    [SerializeField] private GameObject phoneOff, phoneOn, carKeys, caseFile;
    
    [SerializeField] private AudioSource phoneMainAudioSource;
    [SerializeField] private AudioSource phoneDetailsAudioSource;
    [SerializeField] private AudioSource evanTalkingAudioSource;
    [SerializeField] private AudioClip bossVoice, phoneAnswer, phoneHangUp, phoneRingtone, endCall, carDrivingAway, CaseVO;

    [SerializeField] private HintObject phoneOffMat, phoneOnMat, keys, caseFileMat;
    
    
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
        phoneOffMat.TurnHintOnWrapper();
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
        phoneOffMat.TurnHintOff();
        phoneOnMat.TurnHintOnWrapper();
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
        TurnOnCaseFile();
    }
     private void TurnOnCaseFile()
    {
        // Enable Interaction
        caseFile.layer = LayerMask.NameToLayer("Interact");

        // highlight effect on car keys
        caseFileMat.TurnHintOnWrapper();
    }

    private IEnumerator WaitForEvanToFinishTalkingAboutCase()
    {
        // Wait until the boss voice audio finishes playing
        while (evanTalkingAudioSource.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        // After the boss talk is complete, execute the next step
        WaitingForCasefileTalk();
    }

    private void WaitingForCasefileTalk()
    {
        // Enable Interaction
        carKeys.layer = LayerMask.NameToLayer("Interact");

        // highlight effect on car keys
        keys.TurnHintOnWrapper();
    }

    public void PickingUpCase()
    {
         caseFileMat.TurnHintOff();
         caseFile.layer = LayerMask.NameToLayer("Default");   
        // Play car driving away audio
        evanTalkingAudioSource.clip = CaseVO;
        evanTalkingAudioSource.Play();

        StartCoroutine(WaitForEvanToFinishTalkingAboutCase());
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
