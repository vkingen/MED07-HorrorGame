using UnityEngine;

public class WardrobeHint : MonoBehaviour
{
    public Material baseMat;
    public Material highlightMat;
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
        if (highlightMat != null && meshRenderer != null)
        {
            meshRenderer.material = highlightMat;
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
