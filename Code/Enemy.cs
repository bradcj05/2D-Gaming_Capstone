using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     public int health;
     public int defense;
     //Add death animation

     public void TakeDamage (int damage)
     {
          health -= (damage - defense);
          if(health <= 0)
          {
               Die();
          }
     }

     void Die()
     {
          //Play death animation
          Destroy(gameObject);
     }
}
