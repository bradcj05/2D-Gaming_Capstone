/* HOMING MISSILE WITH TIMER */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO improve interface implementation.
[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour, IEnemyProjectile
{
    public float power;
    public float speed;
    public float rotateSpeed = 10f;
    public float rotateAmount;          //public for better testing
    public float penetration;
    public float deterioration;         //ratio/second

    private Transform target;
    private Rigidbody2D rb;
    public float timer;                 //public for better testing
    public float rotationTime = 5f;

    public bool headingDown = false;    // Whether the sprite is heading down
    public bool timed = true;           // Whether the homing is only activated for a certain time

    //For Matt's explosion animation
    public GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    void FixedUpdate()
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        //TODO add hit effect
        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.TakeDamage(power);
            Destroy(gameObject);
        }
    }
}
