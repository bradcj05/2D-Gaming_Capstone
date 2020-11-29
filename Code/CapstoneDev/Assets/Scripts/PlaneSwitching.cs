using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSwitching : MonoBehaviour
{
     public int selectedPlane = 0;

    // Start is called before the first frame update
    void Start()
    {
          SelectPlane(selectedPlane);
    }

    // Update is called once per frame
    void Update()
    {
          int previousPlane = selectedPlane;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
          {
               if (selectedPlane >= transform.childCount - 1)
                    selectedPlane = 0;
               else
                    selectedPlane++;
          }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
          {
               if (selectedPlane <= 0)
                    selectedPlane = transform.childCount - 1;
               else
                    selectedPlane--;
          }

          if (previousPlane != selectedPlane)
               SelectPlane(previousPlane);
    }

     //Changes which of the planes in the squadron are active
     void SelectPlane(int previousPlane)
     {
          //Add Animation
          int i = 0;
          foreach (Transform plane in transform)
          {
               if (i == selectedPlane)
               {
                    plane.gameObject.SetActive(true);

                    //Fixes positioning issues by moving the selected plane to the previous plane's position
                    int p = 0;
                    foreach(Transform prev in transform)
                    {
                         if (p == previousPlane && p != selectedPlane)
                              plane.position = prev.position;
                         p++;
                    }
               }
               else
                    plane.gameObject.SetActive(false);
               i++;
          }
     }
}
