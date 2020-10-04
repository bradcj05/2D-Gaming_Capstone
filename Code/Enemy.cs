using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     public int health;
     public int defense;
     public HealthBar hb;
     //Add death animation

     public Transform gun;
     public GameObject shellType;
     public float bulletSpeed;

     void Start()
     {
          hb.SetMax(health);
     }

     void FixedUpdate()
     {
          //Fix weapon functionality to fire slower
          GameObject bullet = Instantiate(shellType, gun.position, gun.rotation);
          Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
          rig.AddForce(gun.up * bulletSpeed, ForceMode2D.Impulse);
     }

     public void TakeDamage (int damage)
     {
          if((damage - defense) > 0)
          {
               health -= (damage - defense);
          }
          hb.SetHealth(health);

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
