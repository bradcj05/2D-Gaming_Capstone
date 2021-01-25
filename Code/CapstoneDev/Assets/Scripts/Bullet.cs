using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Parameters
    public float power;
    public float speed;
    public float penetration = 0;
    public float deterioration = 0; //ratio/second
    public float selfDestructTime = -1; // Time until self-destruct for effect. Negative to disable
    protected float time = 0;

    // Effects
    public ParticleSystem trailEffect;
    public ParticleSystem engineEffect;
    public ParticleSystem coverEffect;
    public ParticleSystem hitEffect;

    protected Rigidbody2D rb;

    // Base explosion's value for effect
    public float hitEffectRadius = 5f;
    public float hitEffectDuration = 0.5f;
    public float baseExplosionRadius = 3f;

    // Start is called before first frame
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called every frame
    public void Update()
    {
        time += Time.deltaTime;
        // Speed deterioration, destroy without any behavior if expires
        if (deterioration * time >= 1)
        {
            Destroy(gameObject);
        }
        else
        {
            rb.velocity = speed * (1 - deterioration * time) * rb.transform.up;
        }

        // Effect calls
        if (time >= selfDestructTime && selfDestructTime > 0)
        {
            FragShell fs = gameObject.GetComponent<FragShell>();
            Explosion expl = gameObject.GetComponent<Explosion>();

            // Frag shell effect
            if (fs != null)
                fs.Fracture();

            // Explosion effect
            if (expl != null)
                expl.Detonate();

            // Generate hit effect (assuming explosion)
            if (hitEffect != null)
            {
                ParticleSystem curHitEffect = Instantiate(hitEffect, this.transform.position, Quaternion.identity) as ParticleSystem;
                var main = curHitEffect.main;
                main.simulationSpeed = main.duration / hitEffectDuration;
                float scale = hitEffectRadius / baseExplosionRadius;
                curHitEffect.transform.localScale = new Vector3(scale, scale, scale);
                curHitEffect.Play(true);
            }

            // Destroy shell
            Destroy(gameObject);
        }
    }

    // Collision behavior
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO add hit effect
        Destructible e = collision.collider.GetComponent<Destructible>();

        // Effect calls
        FragShell fs = gameObject.GetComponent<FragShell>();
        Explosion expl = gameObject.GetComponent<Explosion>();

        // Hit behavior (allow first frame tolerance so bullet doesn't collide with shooter upon spawn)
        if (e != null && time > 0)
        {
            // Calculate effective defense [Effective defense = defense / cos(angle of contact)]
            ContactPoint2D[] contacts = new ContactPoint2D[2];
            collision.GetContacts(contacts);

            Vector3 normal = contacts[0].normal;
            float penCoeff = Mathf.Abs(Vector3.Cross(rb.velocity.normalized, normal).z);
            // Debug penCoeff for "bullet traps"
            if (penCoeff < 0.2)
            {
                penCoeff = 1 - penCoeff;
            }
            float effectiveDefense = e.defense / penCoeff;

            // Calculate effective damage [Damage = power * (1- det*time)^2 - Max (effective defense - penetration, 0)]
            float damage = power * Mathf.Pow(1 - deterioration * time, 2) - Mathf.Max(effectiveDefense - penetration, 0);
            e.TakeDamage(damage);

            // Determine penetration status
            if (damage > 0)
            {
                // Frag shell effect
                if (fs != null)
                    fs.Fracture();

                // Explosion effect
                if (expl != null)
                    expl.Detonate();

                // Generate hit effect (assuming explosion)
                if (hitEffect != null)
                {
                    ParticleSystem curHitEffect = Instantiate(hitEffect, this.transform.position, Quaternion.identity) as ParticleSystem;
                    var main = curHitEffect.main;
                    main.simulationSpeed = main.duration / hitEffectDuration;
                    float scale = hitEffectRadius / baseExplosionRadius;
                    curHitEffect.transform.localScale = new Vector3(scale, scale, scale);
                    curHitEffect.Play(true);
                }
                Destroy(gameObject);
            }
            // Non-penetration, reflect with energy loss.
            else
            {
                time = time + (1 / deterioration - time) * penCoeff; // HAX
                rb.velocity = Vector3.Reflect(rb.velocity * Mathf.Sqrt(1 - penCoeff), normal);
            }
        }
    }

}