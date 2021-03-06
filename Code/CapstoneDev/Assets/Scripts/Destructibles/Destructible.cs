﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destructible : MonoBehaviour
{
    public float health;
    protected float maxHealth;
    public float defense;
    public HealthBar healthBar; // Health bar
    public HealthBar defenseBar; // Transparent DEFENSE bar

    // Explosion effects
    public ParticleSystem explosion = null;
    public float explosionDuration = 2f;
    public ParticleSystem crater;
    public bool groundCrater = false;
    public GameObject coin;
    protected Animator deathAnimation;
    public bool hasAnimator = false;
    //public AnimationClip airosdeathanim = null;
    //private Animation anim;

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
            Color defenseColor = defenseBar.transform.GetChild(0).GetComponent<Image>().color;
            defenseColor.a = 2f / (1f + Mathf.Exp(-defense / 2)) - 1f;
            defenseBar.transform.GetChild(0).GetComponent<Image>().color = defenseColor;
        }
        maxHealth = health;
        if (healthBar != null)
        {
            healthBar.SetMax(maxHealth);
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
                deathAnimation.SetBool("PlayDeathAnimation", true);
            }

            else Die();
        }
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

        //Actually destroy object
        Destroy(gameObject);
    }

    // HELPER FUNCTION FOR OTHER OBJECTS (e.g. healthbars) THAT NEED TO ACCESS MAX HEALTH
    public float getMaxHealth()
    {
        return maxHealth;
    }
}
