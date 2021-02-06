using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float health;
    protected float maxHealth;
    public float defense;
    public HealthBar healthBar; // Health bar
    public HealthBar defenseBar; // Transparent DEFENSE bar
    
    // Explosion effects
    public ParticleSystem explosion;
    public float explosionDuration = 2f;
    public GameObject crater;
    public GameObject coin;

    public GameObject parent;

    protected Rigidbody2D rb;
    public Vector3 CenterOfMass;

    // Defense bar stuff
    protected bool nonPenetration = false;
    protected float penetrationTimer = 0f;
    protected float penetrationTime = 0.1f;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set health and center of mass
        if (CenterOfMass != null)
            rb.centerOfMass = CenterOfMass;
        if (defenseBar != null)
        {
            defenseBar.SetMax(defense);
        }
        maxHealth = health;
        if (healthBar != null)
        {
            healthBar.SetMax(maxHealth);
        }
    }

    public void Update() {
        // If defense bar is present, set defense bar back after flash
        if (defenseBar != null)
        {
            if (nonPenetration)
            {
                penetrationTimer += Time.deltaTime;
            }
            if (penetrationTimer > penetrationTime)
            {
                nonPenetration = false;
                defenseBar.SetHealth(defense);
            }
        }
    }

    // Damage calculations
    public virtual void TakeDamage(float damage)
    {
        if (damage > 0 && healthBar != null)
        {
            health -= damage;
            healthBar.SetHealth(health);
        }
        else if (damage < 0 && defenseBar != null)
        {
            nonPenetration = true;
            penetrationTimer = 0f;
            defenseBar.SetHealth(-damage);
        }
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

          //Spawn coin if supposed to
          if (coin != null)
               Instantiate(coin, transform.position, transform.rotation);

          //Play explosion
          if (explosion != null)
        {
            ParticleSystem curExplosion = Instantiate(explosion, this.transform.position, explosion.transform.rotation) as ParticleSystem;
            var main = curExplosion.main;
            main.simulationSpeed = main.duration / explosionDuration;
            curExplosion.Play(true);
        }

        //Actually destroy object
        Destroy(gameObject);
    }

    // HELPER FUNCTION FOR OTHER OBJECTS (e.g. healthbars) THAT NEED TO ACCESS MAX HEALTH
    public float getMaxHealth()
    {
        return maxHealth;
    }
}
