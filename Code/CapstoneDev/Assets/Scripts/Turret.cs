using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Finish and Test
public class Turret : MonoBehaviour, Enemy
{
     public float health;
     public float defense;
     public HealthBar hb;
     //Add Explosion when destroyed
     public GameObject crater;

     public Transform bulletSpawn;
     public Transform bulletSpawn2;
     public GameObject shellType;
     public float bulletSpeed;
     public float waitTime = 5f;
     float timer = 0f;

     private Rigidbody2D rb;
     private Transform target;
     public Vector3 CenterOfMass;
     public float rotateSpeed = 1f;
     public float rotateAmount;//public for better testing

     //public float fireRate;
     //public float spread; //In degrees
     //public float powerBuff;
     //public float speedBuff

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          if (CenterOfMass != null)
               rb.centerOfMass = CenterOfMass;
          hb.SetMax(health);
          try
          {
               target = GameObject.FindGameObjectWithTag("Player").transform;
          }
          catch (System.NullReferenceException e)
          {
               Debug.Log(e);
               target = null;
          }
     }

     void Update()
     {
          timer += Time.deltaTime;
          if (timer > waitTime)
          {
               Fire();
               timer -= waitTime;
          }
     }

     void FixedUpdate()
     {
          if(target != null)
          {
               /*
               Vector2 lookDir = target.GetComponent<Rigidbody2D>().position - rb.position;
               float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
               rb.rotation = angle;
               */

               //Causes the Turrets to spin in circles rapidly; Fix
               Vector2 direction = (Vector2)target.position - rb.position;
               direction.Normalize();
               if (Vector3.Dot(direction, transform.up) <= 0)
               {
                    rotateAmount = 1;
               }
               else
               {
                    rotateAmount = Vector3.Cross(direction, transform.up).z;
               }
               float curRot = transform.localRotation.eulerAngles.z;
               transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot - rotateSpeed * rotateAmount));
               
          }
          else
          {
               //Try to find the next player plane when it spawns
               try
               {
                    target = GameObject.FindGameObjectWithTag("Player").transform;
               }
               catch (System.NullReferenceException e)
               {
                    Debug.Log(e);
                    target = null;
               }
          }
     }

     public void Fire()
     {
          GameObject bullet = Instantiate(shellType, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
          Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
          rig.AddForce(bulletSpawn.up * bulletSpeed, ForceMode2D.Impulse);

          GameObject bullet2 = Instantiate(shellType, bulletSpawn2.position, bulletSpawn2.rotation) as GameObject;
          Rigidbody2D rig2 = bullet2.GetComponent<Rigidbody2D>();
          rig2.AddForce(bulletSpawn2.up * bulletSpeed, ForceMode2D.Impulse);
          
     }

     public void TakeDamage(float damage)
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

     public void Die()
     {
          //Play death animation
          Destroy(gameObject);

          //Add crater
          GameObject c = Instantiate(crater, this.transform.position, this.transform.rotation) as GameObject;
          c.transform.parent = GameObject.Find("Terrod").transform;
     }
}
