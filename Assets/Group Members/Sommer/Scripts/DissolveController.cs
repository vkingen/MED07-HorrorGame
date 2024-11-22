using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Material dissolveMaterial; // Assign your material here
    public string dissolveProperty = "_DissolveControl"; // Match the name in Shader Graph
    public float dissolveSpeed = 1.0f; // Speed of dissolve

    private float dissolveValue = 0.0f;

    private float destroyAfter = 10f;

    void Update()
    {
        // Increment the dissolve value
        dissolveValue += Time.deltaTime * dissolveSpeed;
        destroyAfter -= Time.deltaTime;

        // Ensure the value stays within bounds
        dissolveValue = Mathf.Clamp01(dissolveValue);

        // Update the material property
        dissolveMaterial.SetFloat(dissolveProperty, dissolveValue);

        if (destroyAfter <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
