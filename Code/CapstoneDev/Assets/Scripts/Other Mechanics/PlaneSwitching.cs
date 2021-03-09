using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaneSwitching : MonoBehaviour
{
    public int selectedPlane = 0;
    float switchDelay = 2f;
    public float switchTimer; // Public for better testing
    static int squadronSize = 3;
     int size = squadronSize;
    public static GameObject[] squadArr = new GameObject[squadronSize];

    Vector3 startPos;
     protected bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
          if (SceneManager.GetActiveScene().name != "Hangar")
          {
               switchTimer = 0f;
               isDead = false;
               //squadronSize = transform.childCount;
               //Assign numbers to the planes
               //squadArr = new GameObject[transform.childCount];
               for (int i = 0; i < transform.childCount; i++)
               {
                    squadArr[i] = transform.GetChild(i).gameObject;
               }

               //spawn planes in
               for (int i = 0; i < squadronSize; i++)
               {
                    if (squadArr[i] != null)
                    {
                         //squadArr[i] = Instantiate(squadArr[i], transform);
                         //Debug.Log("Setup should work");
                         squadArr[i].GetComponent<Player>().SetUp();
                    }
               }

               //Ensure weapons are active
               for(int i = 0; i < transform.childCount; i++)
               {
                    if (transform.GetChild(i).name == "Airos Player Variant(Clone)")
                    {
                         for (int j = 0; j < transform.GetChild(i).childCount - 2; j++)
                         {
                              transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                         }
                    }
                    else
                    {
                         for (int j = 0; j < transform.GetChild(i).childCount - 1; j++)
                         {
                              transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                         }
                    }
               }
               SelectPlane(new Vector3(0.06f, -11.27f, 0f));
               startPos = new Vector3(0.06f, -11.27f, 0f);
          }
    }

     public void SetUp()
     {
          this.Start();
     }

    // Update is called once per frame
    void Update()
    {
          if(SceneManager.GetActiveScene().name != "Hangar")
          {
               /*/TODO: Change this
               if (transform.childCount == 0)
               {
                    isDead = true;
                    transform.gameObject.SetActive(false); //Just in case
               }*/

               if (switchTimer < switchDelay)
                    switchTimer += Time.deltaTime;

               int previousPlane = selectedPlane;
               //Q and E based plane switching
               if (switchTimer >= switchDelay && Input.GetKeyDown(KeyCode.Q) && transform.childCount != 1)
               {
                    int j = selectedPlane - 1;
                    while (j != selectedPlane)
                    {
                         if (j < 0)
                              j = transform.childCount - 1;

                         if (squadArr[j] != null && squadArr[j].GetComponent<Player>().GetIsDestroyed() == 1)
                         {
                              selectedPlane = j;
                              SelectPlane(squadArr[previousPlane].transform.position);
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

                         if (squadArr[j] != null && squadArr[j].GetComponent<Player>().GetIsDestroyed() == 1)
                         {
                              selectedPlane = j;
                              SelectPlane(squadArr[previousPlane].transform.position);
                              break;
                         }
                         j++;
                    }
                    switchTimer = 0;
               }

               //swap to next plane if previous plane dies
               //TODO: Fix
               if (squadArr[selectedPlane].GetComponent<Player>().GetIsDestroyed() == 0)
               {
                    size--;
                    if(size <= 0)
                    {
                         isDead = true;
                         //transform.gameObject.SetActive(false); //Just in case
                    }

                    //Debug.Log("New PLane should spawn");
                    for (int j = 0; j < squadArr.Length; j++)
                    {
                         if (squadArr[j] != null && squadArr[j].GetComponent<Player>().GetIsDestroyed() == 1)
                         {
                              selectedPlane = j;
                              SelectPlane(startPos);
                              switchTimer = 0f;
                              //size--;
                              Debug.Log("Planes in squadron: " + size);
                              break;
                         }
                    }
               }
          }
    }

    //Changes which of the planes in the squadron are active
    void SelectPlane(Vector3 prevPos)
    {
        //Add Animation
        int i = 0;
        foreach (GameObject plane in squadArr)
        {
            //Loop through the squadron to determine which one is the one to set active
            if (i == selectedPlane)
            {
                plane.SetActive(true);
                plane.tag = "ActivePlayer";
                plane.transform.position = prevPos;
                Player planeObj = plane.GetComponent<Player>();
                // Update health bars
                HealthBar hb = planeObj.healthBar;
                HealthBar db = planeObj.defenseBar;
                if (db != null)
                {
                    db.SetMax(plane.GetComponent<Destructible>().defense);
                }
                if (hb != null)
                {
                    hb.SetMax(plane.GetComponent<Destructible>().getMaxHealth());
                    hb.SetHealth(plane.GetComponent<Destructible>().health);
                }
                // Reset cooldown slider and secondary ammo text accordingly
                if (planeObj.numberOfSecondaryWeapons == 0)
                {
                    planeObj.getCooldownSlider().SetMax(0);
                    planeObj.getSecondaryAmmo().text = "";
                }
            }
            else if (plane != null)
            {
                plane.SetActive(false);
                plane.tag = "Player";
            }
            i++;
        }
    }

     public bool GetIsDead()
     {
          return isDead;
     }
}
