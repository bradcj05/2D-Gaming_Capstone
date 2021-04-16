using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
     ObjectivesSystem objSys;
     public HealthBar jamBar;
     bool towerJammed;
     public float timeNeeded;
     float timer;

     // Start is called before the first frame update
     void Start()
     {
          objSys = GameObject.Find("HUD").GetComponent<ObjectivesSystem>();
          jamBar.SetMax(timeNeeded);
          jamBar.SetHealth(0);
          towerJammed = false;
          timer = 0;
     }

     //Resets the timer when the player enters
     //Made to help reset the bar when a player plane is destroyed in the middle of jamming
     void OnTriggerEnter2D(Collider2D collision)
     {
          if(collision.tag == "ActivePlayer" && !towerJammed)
          {
               //Reset the timer
               timer = 0;
               jamBar.SetHealth(0);
          }
     }

     void OnTriggerStay2D(Collider2D collision)
     {
          if(collision.tag == "ActivePlayer")
          {
               timer += Time.deltaTime;
               jamBar.SetHealth(timer);
               if (timer >= timeNeeded && !towerJammed)
               {
                    towerJammed = true;
                    objSys.TowerUpdate();
               }
          }
     }

     void OnTriggerExit2D(Collider2D collision)
     {
          if(collision.tag == "ActivePlayer" && !towerJammed)
          {
               //Reset the timer
               timer = 0;
               jamBar.SetHealth(0);
          }
     }
}
