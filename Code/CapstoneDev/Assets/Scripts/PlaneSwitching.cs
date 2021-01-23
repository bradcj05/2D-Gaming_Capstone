using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSwitching : MonoBehaviour
{
     public int selectedPlane = 0;
     float switchDelay = 0;
     float switchTimer;
     int squadronSize;
     Transform[] squadArr;

    // Start is called before the first frame update
    void Start()
    {
          SelectPlane(selectedPlane);
          switchTimer = 0;
          squadronSize = transform.childCount;

          //Assign numbers to the planes
          squadArr = new Transform[transform.childCount];
          for(int i = 0; i < transform.childCount; i++)
          {
               squadArr[i] = transform.GetChild(i);
          }
    }

    // Update is called once per frame
    void Update()
    {
          if (transform.childCount == 0)
               Destroy(transform.gameObject);
          int previousPlane = selectedPlane;

          if(switchTimer < switchDelay)
               switchTimer += Time.deltaTime;

          //Q and E based plane switching
          if(switchTimer >= switchDelay && Input.GetKeyDown(KeyCode.Q) && transform.childCount != 1)
          {
               int j = selectedPlane - 1;
               while(j != selectedPlane)
               {
                    if (j < 0)
                         j = transform.childCount - 1;

                    if(squadArr[j] != null)
                    {
                         SelectPlane(selectedPlane);
                         selectedPlane = j;
                         break;
                    }
                    j--;
               }
               switchTimer = 0;
          }
          else if (switchTimer >= switchDelay && Input.GetKeyDown(KeyCode.E) && transform.childCount != 1)
          {
               int j = selectedPlane + 1;
               while (j != selectedPlane)
               {
                    if (j == transform.childCount)
                         j = 0;

                    if (squadArr[j] != null)
                    {
                         SelectPlane(selectedPlane);
                         selectedPlane = j;
                         break;
                    }
                    j++;
               }
               switchTimer = 0;
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
    }

     //Changes which of the planes in the squadron are active
     void SelectPlane(int previousPlane)
     {
          //Add Animation
          int i = 0;
          foreach (Transform plane in transform)
          {
               //Loop through the squadron to determine which one is the one to set active
               if (i == selectedPlane)
               {
                    plane.gameObject.SetActive(true);

                    //Fixes positioning issues by moving the selected plane to the previous plane's position
                    //TODO FIX
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
