using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Finish and test
//To be used with Terrod's Flak shells and other explosive GameObjects
public class Explosion : MonoBehaviour
{
     //public GameObject source;
     public float power = 5f;
     //public float radius = 5f;

     public Vector3 pureKnockbackForce;
     protected Vector3 knockbackForce;

     /*
     void Detonate()
     {
          Vector3 explosionPos = source.transform.position;
          Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
          foreach (Collider hit in colliders)
          {
               Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
               rb.AddExplosionForce(power, explosionPos, radius, 0f, ForceMode.Impulse);
          }
     }*/

     //Damages the player if they collide with the explosion
     void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log(collision.name);
          Player p = collision.GetComponent<Player>();
          if (p != null)
               p.TakeDamage(power);
          Knockback(collision.gameObject);
     }

     //Applys knockback force to objects that collide with the explosion
     void Knockback(GameObject c)
     {
          knockbackForce.x = pureKnockbackForce.x;
          knockbackForce.y = pureKnockbackForce.y;
          try
          {
               Rigidbody2D rb = c.GetComponent<Rigidbody2D>();

               //Make sure the vector for knockback force is pointing in the correct direction
               Vector3 relativePos = c.transform.position - this.transform.position;
               knockbackForce = Vector3.RotateTowards(knockbackForce, relativePos.normalized, 10f, 0f);

               rb.AddForce(knockbackForce, ForceMode2D.Impulse);
          }
          catch (System.Exception e)
          {
               Debug.Log(e);
          }
     }
}
