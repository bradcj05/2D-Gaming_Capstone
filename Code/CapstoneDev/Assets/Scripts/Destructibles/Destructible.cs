using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destructible : MonoBehaviour
{
    public float health;
    protected float maxHealth;
    public float defense;
    protected Billboard enemyHealth;
    public HealthBar healthBar; // Health bar
    public HealthBar defenseBar; // Transparent DEFENSE bar

    // Explosion effects
    public ParticleSystem explosion = null;
    protected ExplosionChain explosionChain;
    public bool hasExplosionChain = false;
    public float explosionDuration = 2f;
    public ParticleSystem crater;
    public bool groundCrater = false;
    public GameObject coin;
    protected Animator deathAnimation;
    public bool hasAnimator = false;

    public GameObject parent;
    protected DestructibleSpawn spawner = null;

    protected Rigidbody2D rb;
    public Vector3 CenterOfMass;

    // Defense bar stuff
    protected bool nonPenetration = false;
    protected float penetrationTimer = 0f;
    protected float penetrationTime = 0.1f;

    // Kinds of objects the destructible can deal collision damage with (see tags)
    public string[] collidableTags; // Can be ActivePlayer, Player, Ally, Enemy, etc.

    // Start is called before the first frame update
    public void Start()
    {
        // Grab rigidbody and healthbars
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Billboard>() != null)
            {
                enemyHealth = child.gameObject.GetComponent<Billboard>();
                healthBar = enemyHealth.transform.GetChild(0).GetComponent<HealthBar>();
                defenseBar = enemyHealth.transform.GetChild(1).GetComponent<HealthBar>();
            }
        }
        // Set health and center of mass
        if (CenterOfMass != null)
            rb.centerOfMass = CenterOfMass;

        if (defenseBar != null)
        {
            defenseBar.SetMax(defense);
            Color defenseColor = defenseBar.transform.GetChild(0).GetComponent<Image>().color;
            defenseColor.a = 2f / (1f + Mathf.Exp(-defense / 2)) - 1f;
            defenseBar.transform.GetChild(0).GetComponent<Image>().color = defenseColor;
        }

        if (maxHealth > 0)
            health = maxHealth;
        else
            maxHealth = health;

        if (healthBar != null)
        {
            healthBar.SetMax(maxHealth);
        }
        // Register explosion chain as a death effect
        if (hasExplosionChain)
        {
            explosionChain = GetComponent<ExplosionChain>();
        }
    }

    public void Update()
    {
    }

    // Damage calculations
    public virtual void TakeDamage(float damage)
    {
        if (damage > 0 && healthBar != null)
        {
            health -= damage;
            healthBar.SetHealth(health);
            defenseBar.SetHealth(defense * health / maxHealth);
        }
        else if (damage < 0 && defenseBar != null)
        {
            defenseBar.SetHealth(-damage * health / maxHealth);
        }
        if (health <= 0)
        {
            if (hasAnimator == true)
            {
                deathAnimation = gameObject.GetComponent<Animator>();
                DeathAnimationProcess();
            }

            else Die();
        }
    }

    // Death animation processor, more for specific enemies
    public void DeathAnimationProcess()
    {
        deathAnimation.SetBool("PlayDeathAnimation", true);
    }

    // Destruction function
    public void Die()
    {
        //Add crater
        if (crater != null)
        {
            if (groundCrater == false)
            {
                crater.Play(true);
            }
            else
            {
                ParticleSystem curCrater = Instantiate(crater, this.transform.position, explosion.transform.rotation) as ParticleSystem;
                curCrater.Play(true);
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

        // Play explosion chain
        if (hasExplosionChain)
        {
            explosionChain.TriggerExplosionChain();
        }

        // Report to spawner that it's dead, if eligible
        if (spawner)
        {
            spawner.SetAlive(false);
        }

        //Actually destroy object
        if (transform.gameObject.GetComponent("Player") != null)
            transform.gameObject.GetComponent<Player>().Die(); //Hopefully this works
        else
            Destroy(gameObject);
    }

    // Getters and setters
    public float getMaxHealth()
    {
        return maxHealth;
    }

    // Setters
    public void SetSpawner(DestructibleSpawn spawner)
    {
        this.spawner = spawner;
    }
}
