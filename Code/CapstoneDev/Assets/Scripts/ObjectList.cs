using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectList : MonoBehaviour
{
     // Lists for each object type, added to in the shop menu, accessed in the hanger menu
     public static List<GameObject> planeList;
     public static List<GameObject> gunList;
     public static List<GameObject> shellList;

     public List<GameObject> planes;
     public List<GameObject> guns;
     public List<GameObject> shells;

    // Start is called before the first frame update
    void Start()
    {
          planeList = planes;
          gunList = guns;
          shellList = shells;
    }

    // Update is called once per frame
    void Update()
    {
          planes = planeList;
          guns = gunList;
          shells = shellList;
     }
}
