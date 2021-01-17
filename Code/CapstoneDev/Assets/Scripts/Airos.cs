using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airos : Enemy /// Always include "Enemy" and "Die()" function
{
    float timer = 0f;

    //Movement Variables
    public float moveSpeed = 5f;
    public Rigidbody2D rig;
    public Rigidbody2D turretL1;
    public Rigidbody2D turretR2;
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
    }

    void FixedUpdate()
    {
        rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}


