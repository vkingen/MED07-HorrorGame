using UnityEngine;

public class ActivateClue : MonoBehaviour
{

    [SerializeField]
    private GameObject clueObj;

    [SerializeField]
    private ClueableObject targetScript;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetScript != null)
        {
            targetScript.isClueActive = true; // Set the bool to true in the target script
        }
    }
}
