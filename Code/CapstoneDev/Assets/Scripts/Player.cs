using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

     public float moveSpeed = 5f;
     public Rigidbody2D rig;
     Vector2 movement;

     public int health;
     public int defense;
     public HealthBar hb;
     //Add death animation


    void Start()
     {
          hb.SetMax(health);
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
     public void TakeDamage(int damage)
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
          //Play death animation
          Destroy(gameObject);

          //Load game over screen / swap to another plane

     }
}
