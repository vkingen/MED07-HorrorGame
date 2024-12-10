using UnityEngine;
using UnityEngine.Events;

public class GameStartEvents : MonoBehaviour
{
   public UnityEvent atGameStart;

   private void Start() 
   {
        atGameStart.Invoke();  
   }
}
