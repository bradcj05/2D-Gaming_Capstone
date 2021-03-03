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

     public Sprite primary;
     public Sprite secondary;
     public GameObject shop;
     public static GameObject primaryArmory;
     public static GameObject secondaryArmory;
     public static GameObject ammoArmory;
     public static GameObject buttonPreFab;

     //Used to set the static values
     public GameObject primaryList;
     public GameObject secondaryList;
     public GameObject ammo;
     public GameObject button;

     //For Button's Stats bars
     public Slider s1;
     public Text st1;
     public Slider s2;
     public Text st2;
     public Slider s3;
     public Text st3;
     public Slider s4;
     public Text st4;

     Image gs;

     void Start()
     {
          i.sprite = ObjectList.planeList[0].artwork;
          index = 0;
          selectionIndex = 0;
          primaryArmory = primaryList;
          secondaryArmory = secondaryList;
          ammoArmory = ammo;
          buttonPreFab = button;

          //Add Sliders to buttons
          buttonPreFab.GetComponent<cardDisplay>().s1 = s1;
          buttonPreFab.GetComponent<cardDisplay>().st1 = st1;
          buttonPreFab.GetComponent<cardDisplay>().s2 = s2;
          buttonPreFab.GetComponent<cardDisplay>().st2 = st2;
          buttonPreFab.GetComponent<cardDisplay>().s3 = s3;
          buttonPreFab.GetComponent<cardDisplay>().st3 = st3;
          buttonPreFab.GetComponent<cardDisplay>().s4 = s4;
          buttonPreFab.GetComponent<cardDisplay>().st4 = st4;

          //Create the Buttons from the Object Lists.
          foreach(Card c in ObjectList.gunList)
          {
               AddButton(c);
          }

          foreach (Card c in ObjectList.shellList)
          {
               AddButton(c);
          }
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

          for (int p = 0; p < selectionIndex; p++)
          {
               switch (p)
               {
                    case 0:
                         foreach (Card current in ObjectList.planeList)
                         {
                              if (current.artwork == plane1.sprite)
                              {
                                   //TODO: Update to allow for positioning of new weapons
                                   Instantiate(current.obj, squadron);
                                   for (int j = 0; j < plane1.transform.childCount; j++)
                                   {
                                        if (plane1.transform.GetChild(j).childCount > 0)
                                        {
                                             Destroy(squadron.GetChild(0).GetChild(j).gameObject);
                                             Instantiate(plane1.transform.GetChild(j).GetChild(0).gameObject, squadron.GetChild(0).transform);
                                        }
                                   }
                                   PlaneSwitching.squadArr[0] = squadron.GetChild(0).gameObject;
                                   break;
                              }
                         }
                         break;
                    case 1:
                         foreach (Card current in ObjectList.planeList)
                         {
                              if (current.artwork == plane2.sprite)
                              {
                                   //TODO: Update to allow for positioning of new weapons
                                   Instantiate(current.obj, squadron);
                                   for (int j = 0; j < plane2.transform.childCount; j++)
                                   {
                                        if (plane2.transform.GetChild(j).childCount > 0)
                                        {
                                             Destroy(squadron.GetChild(1).GetChild(j).gameObject);
                                             Instantiate(plane2.transform.GetChild(j).GetChild(0).gameObject, squadron.GetChild(1).transform);
                                        }
                                   }
                                   PlaneSwitching.squadArr[1] = squadron.GetChild(1).gameObject;
                                   break;
                              }
                         }
                         break;
                    case 2:
                         foreach (Card current in ObjectList.planeList)
                         {
                              if (current.artwork == plane3.sprite)
                              {
                                   //TODO: Update to allow for positioning of new weapons
                                   Instantiate(current.obj, squadron);
                                   for (int j = 0; j < plane3.transform.childCount; j++)
                                   {
                                        if (plane3.transform.GetChild(j).childCount > 0)
                                        {
                                             Destroy(squadron.GetChild(2).GetChild(j).gameObject);
                                             Instantiate(plane3.transform.GetChild(j).GetChild(0).gameObject, squadron.GetChild(2).transform);
                                        }
                                   }
                                   PlaneSwitching.squadArr[2] = squadron.GetChild(2).gameObject;
                                   //Debug.Log(PlaneSwitching.squadArr.Length);
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
          if (slot.sprite == primary)
          {
               shop.SetActive(false);
               primaryArmory.SetActive(true);
               secondaryArmory.SetActive(false);
          }
          else if (slot.sprite == secondary)
          {
               shop.SetActive(false);
               primaryArmory.SetActive(false);
               secondaryArmory.SetActive(true);
          }
          gs = slot;
     }

     //Will need improvements
     public void selectGun(Card c)
     {
          if(gs.transform.childCount > 0)
          {
               Destroy(gs.transform.GetChild(0).gameObject);
          }
          Instantiate(c.obj, gs.transform);

          primaryArmory.SetActive(false);
          secondaryArmory.SetActive(false);
          ammoArmory.SetActive(true);
     }

     //Will need improvements
     public void selectShell(Card c)
     {
          gs.transform.GetChild(0).GetComponent<PlayerGunFire>().shellTypes[0] = c.obj;

          ammoArmory.SetActive(false);
          shop.SetActive(true);
     }

     //Adds the buttons to the object lists
     public static void AddButton(Card c)
     {
          //TODO: Test
          GameObject g = buttonPreFab;
          g.GetComponent<cardDisplay>().cards = c;

          switch ((int)c.cardType)
          {
               case 1:
                    Debug.Log("No Plane List");
                    break;
               case 2:
                    //TODO: Fix
                    Debug.Log((int)c.getCategory());
                    if ((int)c.getCategory() == 1)
                    {
                         Instantiate(g, primaryArmory.transform.GetChild(1).GetChild(0));
                    }
                    else
                    {
                         Instantiate(g, secondaryArmory.transform.GetChild(1).GetChild(0));
                    }
                    break;
               case 3:
                    Instantiate(g, ammoArmory.transform.GetChild(1).GetChild(0));
                    break;
               default:
                    Debug.Log("Please give this card the right card type.");
                    break;
          }
     }
}
