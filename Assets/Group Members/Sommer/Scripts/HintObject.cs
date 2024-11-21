using UnityEngine;

public class HintObject : MonoBehaviour
{
    public Material baseMat;
    public Material hintMat;
    public MeshRenderer meshRenderer;

    void Start()
    {
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer is not assigned in the Inspector!");
        }
    }

    public void TurnHintOn()
    {
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
