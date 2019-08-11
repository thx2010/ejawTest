using UnityEngine;

public class PipeManager : MonoBehaviour
{
   public void Init()
   {
      foreach (Transform child in transform)
      {
         child.Rotate(0, 0, 90 * Random.Range(0, 4));
      }
   }
}
