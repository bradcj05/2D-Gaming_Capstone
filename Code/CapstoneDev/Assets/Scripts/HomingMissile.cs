using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO fix so that it inherits from EnemyBullet
public class HomingMissile : MonoBehaviour
{
     public float power;
     public float speed;
     public float penetration;
     public float deterioration; //ratio/second
     public Transform target;
     private Rigidbody2D rb;

     //For Matt's explosion animation
     public GameObject explosion;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
     }

     void FixedUpdate()
     {

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
