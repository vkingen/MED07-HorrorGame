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
    }
    private void TurnOff()
    {
        isOn = false;
        videoPlayer.GetComponent<MeshRenderer>().material = offMaterial;
        videoPlayer.Stop();
        audioSource.clip = offAudioClip;
        audioSource.Play();
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
