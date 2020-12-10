using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSwitching : MonoBehaviour
{
     public int selectedPlane = 0;
     float switchDelay = 5f;
     float switchTimer;
     int squadronSize;

    // Start is called before the first frame update
    void Start()
    {
          SelectPlane(selectedPlane);
          switchTimer = 0;
          squadronSize = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
          if (transform.childCount == 0)
               Destroy(transform.gameObject);
          int previousPlane = selectedPlane;
          switchTimer += Time.deltaTime;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && switchTimer >= switchDelay)
          {
               if (selectedPlane >= transform.childCount - 1)
                    selectedPlane = 0;
               else
                    selectedPlane++;
          }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && switchTimer >= switchDelay)
          {
               if (selectedPlane <= 0)
                    selectedPlane = transform.childCount - 1;
               else
                    selectedPlane--;
          }

        //TODO swap to next plane if previous plane dies
        //Fix
        if(squadronSize < transform.childCount)
          {
               selectedPlane = 0;
               SelectPlane(previousPlane);
               switchTimer = 0f;
               squadronSize = transform.childCount;
          }

          if (previousPlane != selectedPlane && switchTimer >= switchDelay)
          {
               SelectPlane(previousPlane);
               switchTimer = 0f;
          }
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
