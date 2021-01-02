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
     public float rotateAmount; //public for better testing
     public float penetration;
     public float deterioration; //ratio/second

     public Transform target;
     private Rigidbody2D rb;
     public float timer; //public for better testing
     public float rotationTime = 5f;

     //For Matt's explosion animation
     public GameObject explosion;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          try
          {
               target = GameObject.FindGameObjectWithTag("Player").transform;
          }
          catch(System.NullReferenceException e)
          {
               target = null;
          }
          timer = 0f;
     }

     //Handles the physics and math for the homing missile
     //TODO fix so homing missiles don't loop around forever after the timer ends.
     //TODO figure out torque situation
     void FixedUpdate()
     {
          if (target != null)
          {

               float slowDown = 0.1f;

               if (timer < rotationTime)
               {
                    Vector2 direction = (Vector2)target.position - rb.position;
                    direction.Normalize();
                    rotateAmount = Vector3.Cross(direction, transform.up).z;
                    rb.angularVelocity = -(rotateAmount) * rotateSpeed;

                    rb.AddTorque(-rb.angularVelocity * slowDown, ForceMode2D.Force);

                    timer += Time.deltaTime;
               }
               else
               {
                    //Maybe just destroy gameObject here?
                    rb.angularVelocity = 0;
               }
               rb.velocity = transform.up * speed;
               //rb.AddTorque(slowDown, ForceMode2D.Force);
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
