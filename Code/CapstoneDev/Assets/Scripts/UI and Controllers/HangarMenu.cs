using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangarMenu : MonoBehaviour
{
     int index;
     int selectionIndex;

     //List of objects TODO: use
     public Transform planes;
     //public Transform guns;
     //public Transform shells;

     public GameObject AS57;
     public GameObject P62;
     public GameObject gunR51T;
     public GameObject shellR5AP;

     GameObject selectedPlane;
     GameObject selectedGun;
     GameObject selectedShell;

     void Awake()
     {
          index = 0;
          selectionIndex = 0;
     }

     public void SelectOption()
     {
          //Select Option and move to next part
          //Need to properly implement part selection
          switch (selectionIndex)
          {
               case 0:
                    if(AS57.activeInHierarchy == true)
                    {
                         selectedPlane = AS57;
                    }
                    else
                    {
                         selectedPlane = P62;
                    }
                    AS57.gameObject.SetActive(false);
                    P62.gameObject.SetActive(false);
                    gunR51T.gameObject.SetActive(true);
                    selectionIndex++;
                    index = 0;
                    break;
               case 1:
                    selectedGun = gunR51T;
                    gunR51T.gameObject.SetActive(false);
                    shellR5AP.gameObject.SetActive(true);
                    selectionIndex++;
                    index = 0;
                    break;
               case 2:
                    selectedShell = shellR5AP;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
               default:
                    break;
          }
     }

     public void NextOption()
     {
          //Set Current image to not active
          //Set next image to active

          //Temporary Solution
          if(selectionIndex == 0)
          {
               if (index >= planes.childCount - 1)
                    index = 0;
               else
                    index++;

               int p = 0;
               foreach(Transform current in planes)
               {
                    if (p == index)
                         current.gameObject.SetActive(true);
                    else
                         current.gameObject.SetActive(false);
                    p++;
               }
          }
     }

     public void PreviousOption()
     {
          //Set Current image to not active
          //Set next image to active

          //Temporary Solution
          if (selectionIndex == 0)
          {
               if (index <= 0)
                    index = planes.childCount - 1;
               else
                    index--;

               int p = 0;
               foreach (Transform current in planes)
               {
                    if (p == index)
                         current.gameObject.SetActive(true);
                    else
                         current.gameObject.SetActive(false);
                    p++;
               }
          }
     }

     //May or May not need this function
     void LoadPlane(GameObject plane, GameObject gun, GameObject shell)
     {

     }
}
