using UnityEngine;

public class ActivateClue : MonoBehaviour
{

    [SerializeField]
    private GameObject clueObj;

    [SerializeField]
    private ClueableObject targetScript;

    public float clueTimer = 5f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (clueTimer > 0)
        {
            clueTimer -= Time.deltaTime;
        }
        else
        {
            targetScript.fresnelColor = new Color(1f, 0.5f, 0.5f);
            targetScript.isClueActive = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetScript != null && !targetScript.isClueActive)
        {
            targetScript.isClueActive = true; // Set the bool to true in the target script
        }
    }
}
