// Class to be used for every gun in the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject shellType;
    public float waitTime = 5f; // Time between shots in seconds. Inverse of fire rate.
    protected float timer = 0f;

    public float spread; // In degrees
    public float powerBuff; // In portion of base damage
    public float speedBuff; // In portion of base damage

    // Code to have variable bulletSpawns
    public Transform[] bulletSpawns;

    public void Start() { }

    // Update is called once per frame
    public void Update()
    {
        // Update timer
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            Fire();
            timer -= waitTime;
        }
    }

    // Shoot from each bulletSpawn
    public void Fire()
    {
        foreach (Transform bulletSpawn in bulletSpawns)
        {
            // Account for spread by generating random angle
            float curRot = bulletSpawn.rotation.eulerAngles.z;
            Quaternion bulletAngle = Quaternion.Euler(new Vector3(0, 0, curRot + Random.Range(-spread / 2, spread / 2)));
            // Create bullet
            GameObject bullet = Instantiate(shellType, bulletSpawn.position, bulletAngle) as GameObject;
            Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
            // Apply speed and power buff
            float bulletSpeed = bullet.GetComponent<Bullet>().speed * (1 + speedBuff);
            bullet.GetComponent<Bullet>().power *= (1 + powerBuff);
            // Push bullet
            rig.velocity = bulletSpawn.up * bulletSpeed;
        }
    }
}
