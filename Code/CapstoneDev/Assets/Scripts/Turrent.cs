using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
     //Needs alot of work.

     public int health;
     public int defense;
     public HealthBar hb;
     //Add Explosion when destroyed and crater to replace it
     //public GameObject crater;

     public Transform bulletSpawn;
     public Transform bulletSpawn2;
     public GameObject shellType;
     public float bulletSpeed;
     public float waitTime = 5f;
     float timer = 0f;

     public Rigidbody2D rb;
     public Camera cam;
     //To be replaced with player position later
     Vector2 mousePos;

     void Start()
     {
          hb.SetMax(health);
     }

     //Change rotation to be based on player location
     //Move with the main body
     void Update()
     {
          mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

          timer += Time.deltaTime;
          if (timer > waitTime)
          {
               Fire();
               timer = timer - waitTime;
          }
     }

     void FixedUpdate()
     {
          Vector2 lookDir = mousePos - rb.position;
          float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
          rb.rotation = angle;
     }

     void Fire()
     {
          GameObject bullet = Instantiate(shellType, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
          Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
          rig.AddForce(bulletSpawn.up * bulletSpeed, ForceMode2D.Impulse);

          GameObject bullet2 = Instantiate(shellType, bulletSpawn2.position, bulletSpawn2.rotation) as GameObject;
          Rigidbody2D rig2 = bullet2.GetComponent<Rigidbody2D>();
          rig2.AddForce(bulletSpawn2.up * bulletSpeed, ForceMode2D.Impulse);
          
     }

     public void TakeDamage(int damage)
     {
          if ((damage - defense) > 0)
          {
               health -= (damage - defense);
          }
          hb.SetHealth(health);

          if (health <= 0)
          {
               Die();
          }
     }

     void Die()
     {
          //Play death animation
          Destroy(gameObject);
          //Add crater
     }
}
