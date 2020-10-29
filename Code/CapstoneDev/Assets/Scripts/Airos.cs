using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airos : MonoBehaviour
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

        }
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
