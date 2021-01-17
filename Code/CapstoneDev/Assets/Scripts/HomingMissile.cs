/* HOMING MISSILE WITH TIMER */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO improve interface implementation.
public class HomingMissile : Bullet
{
    public float rotateSpeed = 10f;
    protected float rotateAmount;          //public for better testing

    private Transform target;
    public float timer;                 //public for better testing
    public float rotationTime = 5f;

    public bool headingDown = false;    // Whether the sprite is heading down
    public bool timed = true;           // Whether the homing is only activated for a certain time

    public new void Start()
    {
        base.Start();
        // May have to change player target to something else for allies
        try
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
            target = null;
        }
        timer = 0f;
    }

    //Handles the physics and math for the homing missile
    public void FixedUpdate()
    {
        if (target != null)
        {
            // When homing is active
            if (timer < rotationTime || !timed)
            {
                // Calculate rotateAmount (relative to maximum rotateSpeed) using cross product math
                Vector2 direction = (Vector2)target.position - rb.position;
                direction.Normalize();
                if ((Vector3.Dot(direction, transform.up) <= 0 && !headingDown) || (Vector3.Dot(direction, -transform.up) <= 0 && headingDown))
                {
                    rotateAmount = 1;
                }
                else
                {
                    if (headingDown)
                    {
                        rotateAmount = Vector3.Cross(direction, -transform.up).z;
                    }
                    else
                    {
                        rotateAmount = Vector3.Cross(direction, transform.up).z;
                    }
                }
                // Update timer if time-limited
                if (timed)
                {
                    timer += Time.deltaTime;
                }
            }
            // If homing is inactive, stop rotation
            else
            {
                rotateAmount = 0;
            }
            // Set rotation angle by rotating by rotateSpeed * rotateAmount
            float curRot = transform.localRotation.eulerAngles.z;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot - rotateSpeed * rotateAmount));
        }
        else
        {
            rb.angularVelocity = 0;
        }

        // Move missile forward using velocity
        if (headingDown)
        {
            rb.velocity = -transform.up * speed;
        } 
        else
        {
            rb.velocity = transform.up * speed;
        }
    }
}
