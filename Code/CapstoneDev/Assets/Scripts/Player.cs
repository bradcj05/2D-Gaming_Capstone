using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Destructible
{
    //To get access to scriptable objects information
    public Card cards;
    protected Text nameText;
    public Slider CooldownSlider;
    public float maxSpeed = 5f;
    public float enginePower = 1000f;
    Vector2 movement;
    //Values for rotation
    public Camera cam;
    Vector2 mousePos;

    protected float maxHealth;

    int isDestroyed;
    //Add death animation

    new void Start()
    {
        base.Start();
        //To display name on HealthDock
        nameText.text = cards.name;
        maxHealth = health;
        isDestroyed = 1;
    }

    // Update is called once per frame
    //Input
    new void Update()
    {
        base.Update();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Get Mouse Position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    //Movement
    void FixedUpdate()
    {
        movement.Normalize();
        // Move in the direction specified, then force the speed back to max speed if it is already reached.
        // (Provided the max speed is due to moment and not knockback or external factor)
        rb.AddForce(movement * enginePower, ForceMode2D.Force);
        Vector2 moveDir = rb.velocity / rb.velocity.magnitude;
        if (rb.velocity.magnitude > maxSpeed && movement.magnitude > 0)
        {
            rb.velocity = maxSpeed * moveDir;
        }

        //Rotate the Player
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
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
