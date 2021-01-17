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
    float time = 0;

    // Effects
    public GameObject trailEffect;
    public GameObject engineEffect;
    public GameObject coverEffect;
    public GameObject hitEffect;

    protected Rigidbody2D rb;

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
    }

    // Collision behavior
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        //TODO add hit effect
        Destructible e = collision.GetComponent<Destructible>();

        // Effect calls
        FragShell fs = gameObject.GetComponent<FragShell>();

        // Hit behavior (allow first frame tolerance so bullet doesn't collide with shooter upon spawn)
        if (e != null && time > 0)
        {
            // Frag shell effect
            if (fs != null)
                fs.Fracture();

            // Power calculation accounting for deterioration, penetration, and target's defense
            e.TakeDamage(power * Mathf.Pow(1 - deterioration * time, 2) - Mathf.Max(e.defense - penetration, 0));
            Destroy(gameObject);
        }
    }

}