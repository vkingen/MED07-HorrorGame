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


    public Color fresnelColor; // Color property to set the Fresnel effect color

    public string fresnelColorProperty = "FresnelColor";

    void Start()
    {
        isClueActive = false;
    }
    void Update()
    {
        if (isClueActive)
        {
            if (clueMaterial.HasProperty(fresnelColorProperty))
            {
                clueMaterial.SetColor(fresnelColorProperty, fresnelColor);
            }
            objectRenderer.material = clueMaterial;
        }
        else
        {
            objectRenderer.material = originalMaterial;
        }
    }
}
