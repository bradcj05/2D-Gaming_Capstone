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

     public Image primary;
     public Image secondary;
     public GameObject shop;
     public GameObject primaryArmory;
     public GameObject secondaryArmory;
     public GameObject ammoArmoury;

     Image gs;

     void Start()
     {
          i.sprite = ObjectList.planeList[0].artwork;
          index = 0;
          selectionIndex = 0;
     }

     public void SelectOption()
     {
          //Select Option and move to next part
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

     //Messy may need fixing
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

     //Will need improvements
     public void selectSlot(Image slot)
     {
          if (slot.sprite == primary.sprite)
          {
               shop.SetActive(false);
               primaryArmory.SetActive(true);
          }
          else if (slot.sprite == secondary.sprite)
          {
               shop.SetActive(false);
               secondaryArmory.SetActive(true);
          }
          gs = slot;
     }

     //Will need improvements
     public void selectGun()
     {


          primaryArmory.SetActive(false);
          secondaryArmory.SetActive(false);
          ammoArmoury.SetActive(true);
     }

     //Will need improvements
     public void selectShell()
     {

          ammoArmoury.SetActive(false);
          shop.SetActive(true);
     }
}
