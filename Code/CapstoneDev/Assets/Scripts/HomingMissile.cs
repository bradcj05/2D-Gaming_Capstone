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
     float rotateAmount;
     public float penetration;
     public float deterioration; //ratio/second

     public Transform target;
     private Rigidbody2D rb;
     //TODO Implement timer
     float timer;
     public float rotationTime = 5f;

     //For Matt's explosion animation
     public GameObject explosion;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          target = GameObject.FindGameObjectWithTag("Player").transform;
          timer = 0f;
     }

     //Handles the physics and math for the homing missile
     //TODO test
     void FixedUpdate()
     {
          if (timer < rotationTime)
          {
               Vector2 direction = (Vector2)target.position - rb.position;
               direction.Normalize();
               rotateAmount = Vector3.Cross(direction, transform.up).z;

               timer += Time.deltaTime;
          }
          else
               rotateAmount = 0;
          rb.angularVelocity = -rotateAmount * rotateSpeed;
          rb.velocity = transform.up * speed;
     }

     //TODO: Update to provide bullet functionality
     void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log(collision.name);
          //TODO add bullet damage and hit effect
          Player p = collision.GetComponent<Player>();
          if (p != null)
          {
               p.TakeDamage(power);
               Destroy(gameObject);
          }
     }
}
