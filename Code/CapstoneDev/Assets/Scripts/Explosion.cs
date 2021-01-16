using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Finish and TEST
//To be used with Terrod's Flak shells and other explosive GameObjects
public class Explosion : MonoBehaviour
{
     public GameObject source;
     public float power = 5f;
     public float radius = 5f;

     public float forceThreashold = 5f;
     public Vector3 pureKnockbackForce;
     protected Vector3 knockbackForce;

     //May not need
     void Detonate()
     {
          Collider[] targets = Physics.OverlapSphere(source.transform.position, radius);
          foreach (Collider hit in targets)
          {
               //Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
               //rb.AddExplosionForce(power, explosionPos, radius, 0f, ForceMode.Impulse);
               Vector3 distanceVec = hit.transform.position - transform.position;
               float distance = distanceVec.magnitude;
               float effectivePower = power / (float)System.Math.Pow(distance, 2);
               float effectiveForce = forceThreashold * (float)System.Math.Pow(radius / distance, 2);
               Player p = hit.GetComponent<Player>();
               if (p != null)
                    p.TakeDamage(effectivePower);
               Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
               if(rb != null)
               {
                    distanceVec.Normalize();
                    rb.AddForce(distanceVec * effectiveForce, ForceMode2D.Impulse);
               }
          }
     }

     /*//Damages the player if they collide with the explosion
     void OnTriggerEnter2D(Collider2D collision)
     {
          
          Debug.Log(collision.name);

          Player p = collision.GetComponent<Player>();
          float distance = (source.transform.position - p.transform.position).magnitude; //Change
          if(distance <= radius)
          {
               float effectivePower = power / Math.Pow(distance, 2);
               float effectiveKnockback = forceThreashold * Math.Pow(radius / distance, 2);
               if(p != null)
                    p.TakeDamage(effectivePower);
               Knockback(collision.gameObject, effectiveKnockback); //Change
          }
          
     }
     */
     //Applys knockback force to objects that collide with the explosion
     void Knockback(GameObject c, float effectiveKnockback)
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
