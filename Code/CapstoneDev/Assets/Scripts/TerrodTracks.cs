﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrodTracks : Enemy
{
     protected float maxHealth;
     protected bool isWorking;
     public float repairTime;
     float repairTimer;
     float repairRate; //Percentage of HP after repair

     public new void Start()
     {
          maxHealth = health;
          hb.SetMax(maxHealth);
          isWorking = true;

          repairTimer = 0f;
          repairRate = 0.9f;
     }

     //If the track becomes broken, start repairing it
     public new void Update()
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

     //Damages the GameObject when it collides with a player projectile
     public override void TakeDamage(float damage)
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

     public bool TracksWorking()
     {
          return isWorking;
     }
}
