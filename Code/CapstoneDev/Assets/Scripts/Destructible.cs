using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float health;
    public float defense;
    public HealthBar hb;
    //Add Explosion when destroyed
    public GameObject crater;

    public GameObject parent;

    protected Rigidbody2D rb;
    public Vector3 CenterOfMass;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set health and center of mass
        if (CenterOfMass != null)
            rb.centerOfMass = CenterOfMass;
        hb.SetMax(health);
    }

    public void Update() { }

    // Damage calculations
    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            health -= damage;
        }
        hb.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Destruction function
    public void Die()
    {
        //Play death animation

        //Add crater
        if (crater != null)
        {
            GameObject c = Instantiate(crater, this.transform.position, this.transform.rotation) as GameObject;
            if (parent != null)
            {
                c.transform.parent = parent.transform;
            }
        }

        //Actually destroy object
        Destroy(gameObject);
    }
}
