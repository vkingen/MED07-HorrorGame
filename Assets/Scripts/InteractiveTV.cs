using UnityEngine;
using UnityEngine.Video;

public class InteractiveTV : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip onAudioClip;
    [SerializeField] private AudioClip offAudioClip;

    [SerializeField] private GameObject lightEffect;

    private bool isOn = false;

    public void PerformEvent()
    {
        if (!isOn)
        {
            TurnOn();
        }
    }
    
    private void TurnOn()
    {
        isOn = true;
        videoPlayer.GetComponent<MeshRenderer>().material = onMaterial;
        videoPlayer.Play();
        audioSource.clip = onAudioClip;
        audioSource.Play();
        lightEffect.SetActive(isOn);
    }
    public void TurnOff()
    {
        isOn = false;
        videoPlayer.GetComponent<MeshRenderer>().material = offMaterial;
        videoPlayer.Stop();
        audioSource.clip = offAudioClip;
        audioSource.Play();
        lightEffect.SetActive(isOn);
    }

    public void Toggle()
    {
        isOn = !isOn;
        if (isOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }
}
