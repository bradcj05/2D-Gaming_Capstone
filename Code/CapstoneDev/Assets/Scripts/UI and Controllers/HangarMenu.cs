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
     public Transform squadron;

     //List of objects TODO: use
     //public Transform planes;
     //public Transform guns;
     //public Transform shells;

     //public GameObject AS57;
     //public GameObject P62;
     //public GameObject gunR51T;
     //public GameObject shellR5AP;

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
                    plane1.gameObject.SetActive(true);
                    selectionIndex++;
                    break;
               case 1:
                    if (plane1.sprite != ObjectList.planeList[index].artwork)
                    {
                         plane2.sprite = ObjectList.planeList[index].artwork;
                         plane2.gameObject.SetActive(true);
                         selectionIndex++;
                    }
                    else
                    {
                         Debug.Log("Choose a different plane");
                    }
                    break;
               case 2:
                    if (plane1.sprite != ObjectList.planeList[index].artwork && plane2.sprite != ObjectList.planeList[index].artwork)
                    {
                         plane3.sprite = ObjectList.planeList[index].artwork;
                         plane3.gameObject.SetActive(true);
                         selectionIndex++;
                    }
                    else
                    {
                         Debug.Log("Choose a different plane");
                    }
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
                    plane3.gameObject.SetActive(false);
                    plane3.sprite = null;
                    selectionIndex--;
                    break;
               case 2:
                    plane2.gameObject.SetActive(false);
                    plane2.sprite = null;
                    selectionIndex--;
                    break;
               case 1:
                    plane1.gameObject.SetActive(false);
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
     //Messy will need fixing
     public void LoadPlanes()
     {
          if (selectionIndex < 1)
          {
               Debug.Log("Please add some planes to the squadron.");
               return;
          }

          for (int p = 0; p < 3; p++)
          {
               switch (p)
               {
                    case 0:
                         foreach (Card current in ObjectList.planeList)
                         {
                              if (current.artwork == plane1.sprite)
                              {
                                   PlaneSwitching.squadArr[0] = current.obj;
                                   break;
                              }
                         }
                         break;
                    case 1:
                         foreach (Card current in ObjectList.planeList)
                         {
                              if (current.artwork == plane2.sprite)
                              {
                                   PlaneSwitching.squadArr[1] = current.obj;
                                   break;
                              }
                         }
                         break;
                    case 2:
                         foreach (Card current in ObjectList.planeList)
                         {
                              if (current.artwork == plane3.sprite)
                              {
                                   PlaneSwitching.squadArr[2] = current.obj;
                                   break;
                              }
                         }
                         break;
                    default:
                         break;
               }
          }

          SceneTransition.NextScene();
     }
}
