using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

     public float moveSpeed = 5f;
     public Rigidbody2D rig;
     Vector2 movement;

     public float health;
     public float defense;
     public HealthBar hb;
     int isDestroyed;
     //Add death animation


    void Start()
     {
          hb.SetMax(health);
          isDestroyed = 1;
     }
    // Update is called once per frame
    //Input
    void Update()
    {
          movement.x = Input.GetAxisRaw("Horizontal");
          movement.y = Input.GetAxisRaw("Vertical");
    }

     //Movement
     void FixedUpdate()
     {
          rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
     }

     //Take damage
     public void TakeDamage(float damage)
     {
          if ((damage - defense) > 0)
          {
               health -= (damage - defense);
               if(health < 0)
               {
                    health = 0;
               }
               hb.SetHealth(health);
          }

          if(health <= 0)
          {
               Die();
          }
     }

     //Player Death
     void Die()
     {
          if(isDestroyed == 1)
          {
               //Play death animation
               Destroy(gameObject);
               isDestroyed = 0;
          }

          //Load game over screen / swap to another plane

     }
}
