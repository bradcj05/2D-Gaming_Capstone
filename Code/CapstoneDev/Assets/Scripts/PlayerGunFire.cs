using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{

     public Transform gun;
     public GameObject shellType;
     public float speed;

     public float fireRate; //Shots per second
     public float spread; //in degrees
     public float powerBuff = 0.1f;
     public float speedBuff = 0.1f;
     float timer = 0f;

    // Update is called once per frame
    void Update()
    {
          //Fix
          if(timer < (1000f / fireRate))
          {
               timer += Time.deltaTime;
          }

          if (Input.GetButtonDown("Fire1") && timer >= (1000f / fireRate))
          {
               Fire();
               timer = 0f;
          }
    }

     public void Fire()
     {
          GameObject bullet = Instantiate(shellType, gun.position, gun.rotation);
          //bullet.power = bullet.power * (1 + powerBuff); //Fix
          Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
          rig.AddForce(gun.up * speed, ForceMode2D.Impulse);
     }
}
