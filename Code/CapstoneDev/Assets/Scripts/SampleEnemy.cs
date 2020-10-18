using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour, Enemy
{
     public float health;
     public float defense;
     public HealthBar hb;
     //Add death animation

     public Transform gun;
     public GameObject shellType;
     public float bulletSpeed;
     public float waitTime = 5f;
     float timer = 0f;

     //public float fireRate;
     //public float spread; //In degrees
     //public float powerBuff;
     //public float speedBuff

     void Start()
     {
          hb.SetMax(health);
     }

     void Update()
     {
          timer += Time.deltaTime;
          if (timer > waitTime)
          {
               Fire();
               timer = timer - waitTime;
          }
     }

     public void Fire()
     {
          GameObject bullet = Instantiate(shellType, gun.position, gun.rotation);
          Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
          rig.AddForce(gun.up * bulletSpeed, ForceMode2D.Impulse);
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
     }
}
