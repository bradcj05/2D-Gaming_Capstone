using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HangarMenu : MonoBehaviour
{
     int index;
     int selectionIndex;
     public Image plane1;
     public Image plane2;
     public Image plane3;
     public Image i;

     //List of objects TODO: use
     //public Transform planes;
     //public Transform guns;
     //public Transform shells;

     //public GameObject AS57;
     //public GameObject P62;
     //public GameObject gunR51T;
     //public GameObject shellR5AP;

     GameObject selectedPlane;
     GameObject selectedGun;
     GameObject selectedShell;

     void Start()
     {
          i.sprite = ObjectList.planeList[0].artwork;
          index = 0;
          selectionIndex = 0;
     }

     public void SelectOption()
     {
          //Select Option and move to next part
          //Need to properly implement part selection
          /*switch (selectionIndex)
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
          }*/

          switch (selectionIndex)
          {
               case 0:
                    plane1.sprite = ObjectList.planeList[index].artwork;
                    selectionIndex++;
                    break;
               case 1:
                    plane2.sprite = ObjectList.planeList[index].artwork;
                    selectionIndex++;
                    break;
               case 2:
                    plane3.sprite = ObjectList.planeList[index].artwork;
                    selectionIndex++;
                    break;
               default:
                    break;
          }
     }

     public void DeselectOption()
     {
          switch (selectionIndex)
          {
               case 3:
                    plane3.sprite = null;
                    selectionIndex--;
                    break;
               case 2:
                    plane2.sprite = null;
                    selectionIndex--;
                    break;
               case 1:
                    plane1.sprite = null;
                    selectionIndex--;
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
          /*if(selectionIndex == 0)
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
          }*/

          if (index >= ObjectList.planeList.Count - 1)
               index = 0;
          else
               index++;

          int p = 0;
          foreach(Card current in ObjectList.planeList)
          {
               if (p == index)
               {
                    i.sprite = current.artwork;
                    break;
               }
               p++;
          }
     }

     public void PreviousOption()
     {
          //Set Current image to not active
          //Set next image to active

          //Temporary Solution
          /*if (selectionIndex == 0)
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
          }*/

          if (index <= 0)
               index = ObjectList.planeList.Count - 1;
          else
               index--;

          int p = 0;
          foreach (Card current in ObjectList.planeList)
          {
               if (p == index)
               {
                    i.sprite = current.artwork;
                    break;
               }
               p++;
          }
     }

     //May or May not need this function
     void LoadPlane(GameObject plane, GameObject gun, GameObject shell)
     {

     }
}
