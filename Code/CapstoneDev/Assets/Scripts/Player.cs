using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Destructible
{

    public float maxSpeed = 5f;
    public float acceleration = 1000f;
    Vector2 movement;

    public float maxHealth;
    int isDestroyed;
    //Add death animation


    new void Start()
    {
        base.Start();
        hb.SetMax(maxHealth);
        health = maxHealth;
        isDestroyed = 1;
    }

    // Update is called once per frame
    //Input
    new void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        hb.SetMax(maxHealth);
        hb.SetHealth(health);
    }

    //Movement
    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        movement.Normalize();
        // Move in the direction specified, then force the speed back to max speed if it is already reached.
        // (Provided the max speed is due to moment and not knockback or external factor)
        rb.AddForce(movement * acceleration, ForceMode2D.Force);
        Vector2 moveDir = rb.velocity / rb.velocity.magnitude;
        if (rb.velocity.magnitude > maxSpeed && movement.magnitude > 0)
        {
            rb.velocity = maxSpeed * moveDir;
        }
    }

    //Player Death
    public new void Die()
    {
        if (isDestroyed == 1)
        {
            //Play death animation
            Destroy(gameObject);
            isDestroyed = 0;
        }
    }
}
