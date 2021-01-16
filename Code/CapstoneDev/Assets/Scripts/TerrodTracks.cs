using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrodTracks : MonoBehaviour, Enemy
{
     public float maxHealth;
     public float health;
     public float defense;
     public HealthBar hb;
     private bool isWorking;
     public float repairTime;
     float repairTimer;
     float repairRate; //Percentage of HP after repair

     void Start()
     {
          hb.SetMax(maxHealth);
          health = maxHealth;
          isWorking = true;

          repairTimer = 0f;
          repairRate = 0.9f;
     }

     //If the track becomes broken, start repairing it
     void Update()
     {
          if (!isWorking)
          {
               repairTimer += Time.deltaTime;
               if(repairTimer >= repairTime)
               {
                    health = maxHealth * repairRate;
                    hb.SetHealth(health);
                    if (repairRate > 0.3f)
                         repairRate -= 0.1f;
                    repairTimer = 0f;
                    isWorking = true;
               }
          }
     }

     //Blank on Purpose
     public void Fire() { }

     //Damages the GameObject when it collides with a player projectile
     public void TakeDamage(float damage)
     {
          if ((damage - defense) > 0)
          {
               health -= (damage - defense);
          }
          hb.SetHealth(health);

          if(health <= 0)
          {
               //Set health to 0 just in case health is below 0
               health = 0;
               hb.SetHealth(health);
               isWorking = false;
          }
     }

     //Blank on Purpose
     public void Die() { }

     public bool TracksWorking()
     {
          return isWorking;
     }
}
