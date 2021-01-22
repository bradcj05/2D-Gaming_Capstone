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
    public LayerMask layerMask; // Layer to perform explosion on

    //May not need
    public void Detonate()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        foreach (Collider2D hit in targets)
        {
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
                    p.TakeDamage(Mathf.Max(power,effectivePower) - p.defense);
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
}
