﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sidebars : MonoBehaviour
{
     public Text currentPlaneName;
     public Image currentPlaneIcon;
     public Image currentPlanePrimary;
     public Image currentPlaneSecondary;
     public Text secondaryAmmo0;
     public Image currentPlaneSpecial;
     public Image currentPlaneAmmo;

     //public Text sidePlaneName1;
     public Image sidePlaneIcon1;
     public HealthBar healthBar1;
     public HealthBar defenseBar1;
     public HealthBar cooldownBar1;
     public Image sidePlaneSecondary1;
     public Text secondaryAmmo1;
     public Image sidePlaneSpecial1;

     //public Text sidePlaneName2;
     public Image sidePlaneIcon2;
     public HealthBar healthBar2;
     public HealthBar defenseBar2;
     public HealthBar cooldownBar2;
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
          EquipmentUpdate(0, 0);
          if(PlaneSwitching.squadArr[1] != null && PlaneSwitching.squadArr[2] != null)
          {
               //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
               sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
               healthBar1.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
               healthBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
               defenseBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
               EquipmentUpdate(1, 1);
               //sidePlaneName2.text = PlaneSwitching.squadArr[2].name;
               sidePlaneIcon2.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
               healthBar2.SetMax(PlaneSwitching.squadArr[2].GetComponent<Player>().getMaxHealth());
               healthBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().health);
               defenseBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().defense);
               EquipmentUpdate(2, 2);
          }
          else if (PlaneSwitching.squadArr[1] != null)
          {
               //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
               sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
               healthBar1.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
               healthBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
               defenseBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
               EquipmentUpdate(1, 1);
               //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
               sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
               healthBar2.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
               healthBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
               defenseBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
               EquipmentUpdate(2, 1);
          }
          else
          {
               //sidePlaneName1
               sidePlaneIcon1.gameObject.SetActive(false);
               healthBar1.gameObject.SetActive(false);
               defenseBar1.gameObject.SetActive(false);
               //sidePlaneName2
               sidePlaneIcon2.gameObject.SetActive(false);
               healthBar2.gameObject.SetActive(false);
               defenseBar2.gameObject.SetActive(false);
          }
     }
     
     public void UpdatePlanes(int currentPlane, int initialSize, int[] squadStatus)
     {
          switch (initialSize)
          {
               case 1:
                    break;
               case 2:
                    if (currentPlane == 0 && squadStatus[1] == 1)
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[0].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 0);
                         //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
                         sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         healthBar1.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                         healthBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                         defenseBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                         EquipmentUpdate(1, 1);
                         //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
                         sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         healthBar2.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                         healthBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                         defenseBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                         EquipmentUpdate(2, 1);
                    }
                    else if (currentPlane == 1 && squadStatus[0] == 1)
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[1].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 1);
                         //sidePlaneName1.text = PlaneSwitching.squadArr[0].name;
                         sidePlaneIcon1.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         healthBar1.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                         healthBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                         defenseBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                         EquipmentUpdate(1, 0);
                         //sidePlaneName2.text = PlaneSwitching.squadArr[0].name;
                         sidePlaneIcon2.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         healthBar2.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                         healthBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                         defenseBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                         EquipmentUpdate(2, 0);
                    }
                    else if (currentPlane == 0 && squadStatus[1] == 0)
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[0].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 0);
                         //sidePlaneName1
                         sidePlaneIcon1.gameObject.SetActive(false);
                         healthBar1.gameObject.SetActive(false);
                         defenseBar1.gameObject.SetActive(false);
                         //sidePlaneName2
                         sidePlaneIcon2.gameObject.SetActive(false);
                         healthBar2.gameObject.SetActive(false);
                         defenseBar2.gameObject.SetActive(false);
                    }
                    else
                    {
                         currentPlaneName.text = PlaneSwitching.squadArr[1].name;
                         currentPlaneIcon.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                         EquipmentUpdate(0, 1);
                         //sidePlaneName1
                         sidePlaneIcon1.gameObject.SetActive(false);
                         healthBar1.gameObject.SetActive(false);
                         defenseBar1.gameObject.SetActive(false);
                         //sidePlaneName2
                         sidePlaneIcon2.gameObject.SetActive(false);
                         healthBar2.gameObject.SetActive(false);
                         defenseBar2.gameObject.SetActive(false);
                    }
                    break;
               case 3:
                    switch (currentPlane)
                    {
                         case 0:
                              currentPlaneName.text = PlaneSwitching.squadArr[0].name;
                              currentPlaneIcon.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                              EquipmentUpdate(0, 0);
                              if (squadStatus[1] == 1 && squadStatus[2] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 1);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[2].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[2].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 2);
                              }
                              else if (squadStatus[1] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 1);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 1);
                              }
                              else if (squadStatus[2] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[2].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[2].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 2);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[2].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[2].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 2);
                              }
                              else
                              {
                                   //sidePlaneName1
                                   sidePlaneIcon1.gameObject.SetActive(false);
                                   healthBar1.gameObject.SetActive(false);
                                   defenseBar1.gameObject.SetActive(false);
                                   //sidePlaneName2
                                   sidePlaneIcon2.gameObject.SetActive(false);
                                   healthBar2.gameObject.SetActive(false);
                                   defenseBar2.gameObject.SetActive(false);
                              }
                              break;
                         case 1:
                              currentPlaneName.text = PlaneSwitching.squadArr[1].name;
                              currentPlaneIcon.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                              EquipmentUpdate(0, 1);
                              if (squadStatus[2] == 1 && squadStatus[0] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[2].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[2].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 2);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[0].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 0);
                              }
                              else if (squadStatus[0] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[0].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 0);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[0].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 0);
                              }
                              else if (squadStatus[2] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[2].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[2].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 2);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[2].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[2].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[2].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 2);
                              }
                              else
                              {
                                   //sidePlaneName1
                                   sidePlaneIcon1.gameObject.SetActive(false);
                                   healthBar1.gameObject.SetActive(false);
                                   defenseBar1.gameObject.SetActive(false);
                                   //sidePlaneName2
                                   sidePlaneIcon2.gameObject.SetActive(false);
                                   healthBar2.gameObject.SetActive(false);
                                   defenseBar2.gameObject.SetActive(false);
                              }
                              break;
                         case 2:
                              currentPlaneName.text = PlaneSwitching.squadArr[2].name;
                              currentPlaneIcon.sprite = PlaneSwitching.squadArr[2].GetComponent<SpriteRenderer>().sprite;
                              EquipmentUpdate(0, 2);
                              if (squadStatus[1] == 1 && squadStatus[0] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[0].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 0);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 1);
                              }
                              else if (squadStatus[0] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[0].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 0);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[0].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[0].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[0].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[0].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 0);
                              }
                              else if (squadStatus[1] == 1)
                              {
                                   //sidePlaneName1.text = PlaneSwitching.squadArr[1].name;
                                   sidePlaneIcon1.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                                   healthBar1.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                                   healthBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                                   defenseBar1.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                                   EquipmentUpdate(1, 1);
                                   //sidePlaneName2.text = PlaneSwitching.squadArr[1].name;
                                   sidePlaneIcon2.sprite = PlaneSwitching.squadArr[1].GetComponent<SpriteRenderer>().sprite;
                                   healthBar2.SetMax(PlaneSwitching.squadArr[1].GetComponent<Player>().getMaxHealth());
                                   healthBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().health);
                                   defenseBar2.SetHealth(PlaneSwitching.squadArr[1].GetComponent<Player>().defense);
                                   EquipmentUpdate(2, 1);
                              }
                              else
                              {
                                   //sidePlaneName1
                                   sidePlaneIcon1.gameObject.SetActive(false);
                                   healthBar1.gameObject.SetActive(false);
                                   defenseBar1.gameObject.SetActive(false);
                                   //sidePlaneName2
                                   sidePlaneIcon2.gameObject.SetActive(false);
                                   healthBar2.gameObject.SetActive(false);
                                   defenseBar2.gameObject.SetActive(false);
                              }
                              break;
                         default:
                              break;
                    }
                    break;
               default:
                    break;
          }
     }

     //May need to update to account for Airos
     void EquipmentUpdate(int slot, int plane)
     {
          int weaponSlots = 0;
          if (PlaneSwitching.squadArr[plane].name == "Airos")
               weaponSlots = PlaneSwitching.squadArr[plane].transform.childCount - 2;
          else
               weaponSlots = PlaneSwitching.squadArr[plane].transform.childCount - 1;

          switch (slot)
          {
               case 0:
                    currentPlanePrimary.gameObject.SetActive(false);
                    currentPlaneSecondary.gameObject.SetActive(false);
                    currentPlaneAmmo.gameObject.SetActive(false);
                    secondaryAmmo0.text = "0";
                    currentPlaneSpecial.gameObject.SetActive(false);

                    for(int i = 0; i < weaponSlots; i++)
                    {
                         if (PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent("PlayerLaser") == null)
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
                         }
                         else
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
                    cooldownBar1.SetHealth(0);

                    for(int i = 0; i < weaponSlots; i++)
                    {
                         if (PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent("PlayerLaser") == null)
                         {
                              if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 2 ||
                              (int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 3)
                              {
                                   sidePlaneSecondary1.gameObject.SetActive(true);
                                   sidePlaneSecondary1.sprite = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                                   secondaryAmmo1.text = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().ammo.ToString();
                                   cooldownBar1.SetMax(PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SecondaryWeapon>().waitTime);
                                   cooldownBar1.SetHealth(PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SecondaryWeapon>().GetTimerValue());
                              }
                         }
                         else
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
                    cooldownBar2.SetHealth(0);

                    for (int i = 0; i < weaponSlots; i++)
                    {
                         if (PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent("PlayerLaser") == null)
                         {
                              if ((int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 2 ||
                              (int)PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().category == 3)
                              {
                                   sidePlaneSecondary2.gameObject.SetActive(true);
                                   sidePlaneSecondary2.sprite = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                                   secondaryAmmo2.text = PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<Gun>().ammo.ToString();
                                   cooldownBar2.SetMax(PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SecondaryWeapon>().waitTime);
                                   cooldownBar2.SetHealth(PlaneSwitching.squadArr[plane].transform.GetChild(i).GetChild(0).GetComponent<SecondaryWeapon>().GetTimerValue());
                              }
                         }
                         else
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

     public void Reset()
     {
          this.Start();
     }
}
