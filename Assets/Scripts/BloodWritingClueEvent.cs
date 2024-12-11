using UnityEngine;

public class BloodWritingClueEvent : MonoBehaviour
{
    [SerializeField] private AnimateBloodWriting writing1, writing2;


    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.U))
    //    {
    //        //Debug.Log("START WRITE");
    //        PerformEvent();
    //    }
    //}
    public void PerformEvent()
    {
        StartCoroutine(PerformWritingSequence());
    }

    private System.Collections.IEnumerator PerformWritingSequence()
    {
        // Start the first writing
        writing1.Write();

        // Wait until writing1 is done
        yield return new WaitUntil(() => writing1.isDone);

        // Start the second writing
        writing2.Write();

        // Optionally wait for writing2 to finish as well (if needed)
        yield return new WaitUntil(() => writing2.isDone);

        Debug.Log("Both writings are complete!");
    }
}
