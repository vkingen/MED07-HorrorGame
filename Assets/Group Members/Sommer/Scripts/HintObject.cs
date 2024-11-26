using System.Collections;
using UnityEngine;

public class HintObject : MonoBehaviour
{
    public Material baseMat;
    public Material hintMat;
    public MeshRenderer meshRenderer;

    [SerializeField] private float waitTime;

    void Start()
    {
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer is not assigned in the Inspector!");
        }
    }

    public void TurnHintOnWrapper()
    {
        StartCoroutine(nameof(TurnHintOn));
    }

    public IEnumerator TurnHintOn()
    {
        yield return new WaitForSeconds(waitTime);
        if (hintMat != null && meshRenderer != null)
        {
            meshRenderer.material = hintMat;
        }
    }

    public void TurnHintOff()
    {
        if (baseMat != null && meshRenderer != null)
        {
            meshRenderer.material = baseMat;
        }
    }
}
