using UnityEngine;

public class PoolBreakEvent : MonoBehaviour
{

    public AudioSource audioSource;
    public GameObject poolBalls;
    public GameObject oldBalls;
    public GameObject oldBilliardCue;

    public void PlayPoolEvent()
    {
        if (audioSource != null)
            audioSource.Play();
        else
            Debug.LogError("AudioSource component is missing! Please add one to this GameObject.");

        if(poolBalls != null){
            poolBalls.SetActive(true);
            oldBalls.SetActive(false);
            oldBilliardCue.SetActive(false);
        }
    }
}
