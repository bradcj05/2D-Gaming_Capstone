using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Finish and TEST
//To be used with Terrod's Flak shells and other explosive GameObjects
public class Explosion : MonoBehaviour
{
    public float power = 5f;
    public float radius = 5f;

    public float forceThreshold = 0.5f;

    public bool decentralized = false; // Whether the explosion deals constant damage among radius.

    //May not need
    public void Detonate()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Player"));
        foreach (Collider2D hit in targets)
        {
            //Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            //rb.AddExplosionForce(power, explosionPos, radius, 0f, ForceMode.Impulse);
            Vector3 distanceVec = hit.transform.position - transform.position;
            float distance = distanceVec.magnitude;
            if (distance > 0)
            {
                // Calculate effective power and force
                float effectivePower;
                float effectiveForce;
                if (decentralized)
                {
                    effectivePower = power;
                    effectiveForce = 0;
                }
                else
                {
                    effectivePower = power / Mathf.Pow(distance, 2);
                    effectiveForce = forceThreshold * Mathf.Pow(radius / distance, 2);
                }
                // Deal explosion damage
                Destructible p = hit.GetComponent<Destructible>();
                if (p != null)
                {
                    p.TakeDamage(effectivePower - p.defense);
                }
                // Knockback
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    distanceVec.Normalize();
                    rb.AddForce(effectiveForce * distanceVec, ForceMode2D.Force);
                }
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
              float effectiveKnockback = forceThreshold * Math.Pow(radius / distance, 2);
              if(p != null)
                   p.TakeDamage(effectivePower);
              Knockback(collision.gameObject, effectiveKnockback); //Change
         }

    }
    */
}
