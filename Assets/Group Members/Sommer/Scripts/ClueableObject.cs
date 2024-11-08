using UnityEngine;

public class ClueableObject : MonoBehaviour
{
    public bool isClueActive;

    [SerializeField]
    private Material clueMaterial;

    [SerializeField]
    private MeshRenderer objectRenderer;

    [SerializeField]
    private Material originalMaterial;

    void Start()
    {
        isClueActive = false;
    }
    void Update()
    {
        if (isClueActive)
        {
            objectRenderer.material = clueMaterial;
        }
        else
        {
            objectRenderer.material = originalMaterial;
        }
    }
}
