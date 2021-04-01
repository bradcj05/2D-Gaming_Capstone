using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sidebars : MonoBehaviour
{
     //Needs to be Tested and Fixed
     public Text currentPlaneName;
     public Image currentPlaneIcon;
     public HealthBar hb0;
     public Image currentPlanePrimary;
     public Image currentPlaneSecondary;
     public Text secondaryAmmo0;
     public Image currentPlaneSpecial;
     public Image currentPlaneAmmo;

     public Text sidePlaneName1;
     public Image sidePlaneIcon1;
     public Image sidePlaneSecondary1;
     public Text secondaryAmmo1;
     public Image sidePlaneSpecial1;

     public Text sidePlaneName2;
     public Image sidePlaneIcon2;
     public Image sidePlaneSecondary2;
     public Text secondaryAmmo2;
     public Image sidePlaneSpecial2;

     public Sprite laserSprite;

     // Start is called before the first frame update
     void Start()
     {
          //Find the initial plane
          currentPlaneName.text = PlaneSwitching.squadArr[0].name;
          currentPlaneIcon.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
          //hb0.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
          //hb0.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
          EquipmentUpdate(0, 0);
          if(PlaneSwitching.squadArr[1] != null && PlaneSwitching.squadArr[2] != null)
          {
               //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
               sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
               EquipmentUpdate(1, 1);
               //sidePlaneName2.text = PlaneSwitching.squadArr[2].name;
               sidePlaneIcon2.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
               EquipmentUpdate(2, 2);
          }
          else if (PlaneSwitching.squadArr[1] != null)
          {
               //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
               sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
               EquipmentUpdate(1, 1);
               //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
               sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
               EquipmentUpdate(2, 1);
          }
          else
          {
               //sidePlaneName1.text = PlaneSwitching.squadArr[0].name;
               sidePlaneIcon1.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
               EquipmentUpdate(1, 0);
               //sidePlaneName2.text = PlaneSwitching.squadArr[0].name;
               sidePlaneIcon2.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
               EquipmentUpdate(2, 0);
          }
     }

     public void UpdatePlanes(int currentPlane)
     {
          switch (PlaneSwitching.squadArr.Length)
          {
               case 1:
                    break;
               case 2:
                    if (currentPlane == 0)
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[0].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 0);
                         //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
                         sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(1, 1);
                         //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
                         sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(2, 2);
                    }
                    else
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[1].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 1);
                         //sidePlaneName1.text = PlaneSwitching.squadArr[0].name;
                         sidePlaneIcon1.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(1, 0);
                         //sidePlaneName2.text = PlaneSwitching.squadArr[0].name;
                         sidePlaneIcon2.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(2, 0);
                    }
                    break;
               case 3:
                    if (currentPlane == 0)
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[0].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 0);
                         //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
                         sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(1, 1);
                         //sidePlaneName2.text = PlaneSwitching.squadArr[2].name;
                         sidePlaneIcon2.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(2, 2);
                    }
                    else if (currentPlane == 1)
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[1].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 1);
                         //sidePlaneName1.text = PlaneSwitching.squadArr[2].name;
                         sidePlaneIcon1.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(1, 2);
                         //sidePlaneName2.text = PlaneSwitching.squadArr[0].name;
                         sidePlaneIcon2.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(2, 0);
                    }
                    else
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[2].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 2);
                         //sidePlaneName1.text = PlaneSwitching.squadArr[0].name;
                         sidePlaneIcon1.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(1, 0);
                         //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
                         sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(2, 1);
                    }
                    break;
               default:
                    break;
          }
     }

     //May need to update to account for Airos
     void EquipmentUpdate(int slot, int plane)
     {
          switch (slot)
          {
               case 0:
                    currentPlanePrimary.gameObject.SetActive(false);
                    currentPlaneSecondary.gameObject.SetActive(false);
                    currentPlaneAmmo.gameObject.SetActive(false);
                    secondaryAmmo0.text = "0";
                    currentPlaneSpecial.gameObject.SetActive(false);

                    for(int i = 0; i < PlaneSwitching.squadArr[plane].transform.childCount - 1; i++)
                    {
                         if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 1)
                         {
                              currentPlanePrimary.gameObject.SetActive(true);
                              currentPlanePrimary.sprite = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                         }
                         else if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 2 ||
                              (int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 3)
                         {
                              currentPlaneSecondary.gameObject.SetActive(true);
                              currentPlaneSecondary.sprite = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                              currentPlaneAmmo.gameObject.SetActive(true);
                              currentPlaneAmmo.sprite = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().shellTypes[0].GetComponent<SpriteRenderer>().sprite;
                              secondaryAmmo0.text = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().ammo.ToString();
                         }
                         else if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().grade == 3)
                         {
                              currentPlaneSpecial.gameObject.SetActive(true);
                              currentPlaneSpecial.sprite = laserSprite;
                         }
                    }
                    break;
               case 1:
                    sidePlaneSecondary1.gameObject.SetActive(false);
                    secondaryAmmo1.text = "0";
                    sidePlaneSpecial1.gameObject.SetActive(false);

                    for(int i = 0; i < PlaneSwitching.squadArr[plane].transform.childCount - 1; i++)
                    {
                         if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 2 ||
                              (int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 3)
                         {
                              sidePlaneSecondary1.gameObject.SetActive(true);
                              sidePlaneSecondary1.sprite = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                              secondaryAmmo1.text = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().ammo.ToString();
                         }
                         else if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().grade == 3)
                         {
                              sidePlaneSpecial1.gameObject.SetActive(true);
                              sidePlaneSpecial1.sprite = laserSprite;
                         }
                    }
                    break;
               case 2:
                    sidePlaneSecondary2.gameObject.SetActive(false);
                    secondaryAmmo2.text = "0";
                    sidePlaneSpecial2.gameObject.SetActive(false);

                    for (int i = 0; i < PlaneSwitching.squadArr[plane].transform.childCount - 1; i++)
                    {
                         if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 2 ||
                              (int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 3)
                         {
                              sidePlaneSecondary2.gameObject.SetActive(true);
                              sidePlaneSecondary2.sprite = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                              secondaryAmmo2.text = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().ammo.ToString();
                         }
                         else if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().grade == 3)
                         {
                              sidePlaneSpecial2.gameObject.SetActive(true);
                              sidePlaneSpecial2.sprite = laserSprite;
                         }
                    }
                    break;
               default:
                    Debug.Log("This message should not appear. Issue with EquipmentUpdate in the Sidebars script.");
                    break;
          }
     }
}
