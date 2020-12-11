using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirosTurret : MonoBehaviour, Enemy
{
    public float health;
    public float defense;
    public HealthBar hb;
    //Add Explosion when destroyed
    public GameObject crater;

    public Transform bulletSpawn;
    public Transform bulletSpawn2;
    public Transform bulletSpawn3;
    public GameObject shellType;
    public float bulletSpeed;
    public float waitTime = 5f;
    float timer = 0f;

    public Rigidbody2D rb;
    public GameObject player;
    public Vector3 CenterOfMass;

    //public float fireRate;
    //public float spread; //In degrees
    //public float powerBuff;
    //public float speedBuff

    void Start()
    {
        if (CenterOfMass != null)
            rb.centerOfMass = CenterOfMass;
        hb.SetMax(health);
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            Fire();
            timer = timer - waitTime;
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 lookDir = player.GetComponent<Rigidbody2D>().position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(shellType, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        rig.AddForce(bulletSpawn.up * bulletSpeed, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(shellType, bulletSpawn2.position, bulletSpawn2.rotation) as GameObject;
        Rigidbody2D rig2 = bullet2.GetComponent<Rigidbody2D>();
        rig2.AddForce(bulletSpawn2.up * bulletSpeed, ForceMode2D.Impulse);

        GameObject bullet3 = Instantiate(shellType, bulletSpawn3.position, bulletSpawn3.rotation) as GameObject;
        Rigidbody2D rig3 = bullet3.GetComponent<Rigidbody2D>();
        rig3.AddForce(bulletSpawn3.up * bulletSpeed, ForceMode2D.Impulse);

    }

    public void TakeDamage(float damage)
    {
        if ((damage - defense) > 0)
        {
            health -= (damage - defense); ///defense is too high for damage calculation 5-5=0
        }
        hb.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Play death animation
        Destroy(gameObject);

        //Add crater
        GameObject c = Instantiate(crater, this.transform.position, this.transform.rotation) as GameObject;
        c.transform.parent = GameObject.Find("Airos").transform;
    }
}
