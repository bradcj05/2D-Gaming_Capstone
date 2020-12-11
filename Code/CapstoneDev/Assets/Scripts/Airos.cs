using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airos : MonoBehaviour, Enemy /// Always include "Enemy" and "Die()" function
{
    public float health;
    public float defense;
    public HealthBar hb;
    //Add death animation

    public Transform gun;
    public Transform bulletSpawn;
    public Transform bulletSpawn2;
    public Transform bulletSpawn3;
    public Transform bulletSpawn4;   // turn into an array, step timer, module 3
    public Transform bulletSpawn5;
    public Transform bulletSpawn6;
    public GameObject shellType;
    public float bulletSpeed;
    public float waitTime = 6f;
   // public float shootTime = .5f;
    float timer = 0f;
   // float fire = 0f;

    //Movement Variables
    public float moveSpeed = 5f;
    public Rigidbody2D rig;
    public Rigidbody2D turretL1;
    public Rigidbody2D turretR2;
    Vector3 playerPos;
    Vector2 movement;
    float moveWaitTime = 5f;
    float moveTimer = 0f;


    void Start()
    {
       
        movement.x = -0.6f;
        }

    void Update()
    {
        if (movement.x < 0 && rig.position.x <= -6.5)
        {
            movement.x = 0.6f;
        }
        else if (movement.x > 0 && rig.position.x >= 6.5)
        {
            movement.x = -0.6f;
        }

        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            Fire();
            timer = timer - waitTime;
        }
        
    }

    
    public void Fire()
    {

        
        GameObject bullet = Instantiate(shellType, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        Rigidbody2D rig7 = bullet.GetComponent<Rigidbody2D>();
        rig7.AddForce(bulletSpawn.up * bulletSpeed, ForceMode2D.Impulse);

        GameObject bullet6 = Instantiate(shellType, bulletSpawn6.position, bulletSpawn6.rotation) as GameObject;
        Rigidbody2D rig6 = bullet6.GetComponent<Rigidbody2D>();
        rig6.AddForce(bulletSpawn6.up * bulletSpeed, ForceMode2D.Impulse);
        

       
            GameObject bullet3 = Instantiate(shellType, bulletSpawn3.position, bulletSpawn3.rotation) as GameObject;
            Rigidbody2D rig3 = bullet3.GetComponent<Rigidbody2D>();
            rig3.AddForce(bulletSpawn3.up * bulletSpeed, ForceMode2D.Impulse);

            GameObject bullet4 = Instantiate(shellType, bulletSpawn4.position, bulletSpawn4.rotation) as GameObject;
            Rigidbody2D rig4 = bullet4.GetComponent<Rigidbody2D>();
            rig4.AddForce(bulletSpawn4.up * bulletSpeed, ForceMode2D.Impulse);

            
                GameObject bullet5 = Instantiate(shellType, bulletSpawn5.position, bulletSpawn5.rotation) as GameObject;
                Rigidbody2D rig5 = bullet5.GetComponent<Rigidbody2D>();
                rig5.AddForce(bulletSpawn5.up * bulletSpeed, ForceMode2D.Impulse);

                GameObject bullet2 = Instantiate(shellType, bulletSpawn2.position, bulletSpawn2.rotation) as GameObject;
                Rigidbody2D rig2 = bullet2.GetComponent<Rigidbody2D>();
                rig2.AddForce(bulletSpawn2.up * bulletSpeed, ForceMode2D.Impulse);
          
            
    }

    public void TakeDamage(float damage)
    {
        if ((damage - defense) > 0)
        {
            health -= (damage - defense); ///defense is too high for damage calculation 5-5=0
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
        void FixedUpdate()
        {
            rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
          if(turretL1 != null)
               turretL1.MovePosition(turretL1.position + movement * moveSpeed * Time.fixedDeltaTime);
          if(turretR2 != null)
               turretR2.MovePosition(turretR2.position + movement * moveSpeed * Time.fixedDeltaTime);
          
        }
    }


   