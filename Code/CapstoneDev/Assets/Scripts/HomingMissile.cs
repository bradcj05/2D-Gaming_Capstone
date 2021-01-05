using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO fix so that it inherits from EnemyBullet or another interface
[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
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
     void FixedUpdate()
     {
          if (target != null)
          {

               //tutorial version with timer
               
               if (timer < rotationTime)
               {
                    Vector2 direction = (Vector2)target.position - rb.position;
                    direction.Normalize();
                    if(Vector3.Dot(direction, transform.up) <= 0)
                    {
                        rotateAmount = 1;
                    } 
                    else
                    {
                        rotateAmount = Vector3.Cross(direction, transform.up).z;
                    }
                    timer += Time.deltaTime;
               }
               else
               {
                    //Maybe just destroy gameObject here?
                    rotateAmount = 0;
               }
                float curRot = transform.localRotation.eulerAngles.z;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot - rotateSpeed * rotateAmount));
                //rb.angularVelocity = -rotateSpeed * rotateAmount;
                rb.velocity = transform.up * speed;


               //Tutorial version without timer
               /*
               Vector2 direction = (Vector2)target.position - rb.position;
               direction.Normalize();
               rotateAmount = Vector3.Cross(direction, transform.up).z;
               rb.angularVelocity = -rotateAmount * rotateSpeed;
               rb.velocity = transform.up * speed;
               */

               //Different version
               //Vector2 direction = (Vector2)target.position - rb.position;

          }
          else
          {
               rb.angularVelocity = 0;
               rb.velocity = transform.up * speed;
          }
     }

     void OnTriggerEnter2D(Collider2D collision)
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
