using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
     public static bool[] progress;

    // Start is called before the first frame update
    void Awake()
    {
          //Update to have more later if need be.
          progress = new bool[10];
          for(int b = 0; b < progress.Length; b++)
          {
               progress[b] = false;
          }
    }
}
