using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Destructible
{

    public float moveSpeed = 5f;
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
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        hb.SetMax(maxHealth);
        hb.SetHealth(health);
    }

    //Movement
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    //Player Death
    new void Die()
    {
        if (isDestroyed == 1)
        {
            //Play death animation
            Destroy(gameObject);
            isDestroyed = 0;
        }
    }
}
