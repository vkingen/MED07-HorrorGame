using UnityEngine;

public class AnimateBloodWriting : MonoBehaviour
{
    public Material writingMaterial; // Assign the material here
    public float revealSpeed = 0.5f; // Speed of the reveal animation

    private float revealProgress = 1;
    private bool isStarted = false;
    public bool isDone = false;
    private void Start()
    {
        revealProgress = 1;
        writingMaterial.SetFloat("_RevealProgress", revealProgress);
    }

    public void Write()
    {
        isStarted = true;
    }

    void Update()
    {
        if (isStarted)
        {
            if (revealProgress > 0 && !isDone)
            {
                revealProgress -= Time.deltaTime * revealSpeed;
                writingMaterial.SetFloat("_RevealProgress", revealProgress);
                if (revealProgress <= 0)
                {
                    isDone = true;
                }
            }
        }
    }
}
