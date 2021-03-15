// Class to be used for every gun in the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponsClassification
{
    public GameObject[] shellTypes;
    public float waitTime = 5f; // Time between shots in seconds. Inverse of fire rate.
    protected float timer = 0f;

    public float spread; // In degrees
    public float powerBuff; // In portion of base damage
    public float speedBuff; // In portion of base damage
    public float recoilForce = 0f;

    public int ammo = -1; // Negative for infinite
     static int maxAmmo;

    // Code to have variable bulletSpawns
    public Transform[] bulletSpawns;

    public new void Start()
     {
          base.Start();
          if (maxAmmo != null && maxAmmo > 0)
               ammo = maxAmmo;
          else
               maxAmmo = ammo;
     }

     public void SetUp()
     {
          this.Start();
     }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
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
            Fire(bulletSpawn);
        }
    }

    protected void Fire(Transform bulletSpawn)
    {
        Fire(bulletSpawn, shellTypes[0]);
    }

    // Stock method to fire specific bullet for all guns
    protected void Fire(GameObject shellType)
    {
        foreach (Transform bulletSpawn in bulletSpawns)
        {
            Fire(bulletSpawn, shellType);
        }
    }

    // Stock method to fire specific bullet at a specific gun
    protected void Fire(Transform bulletSpawn, GameObject shellType)
    {
        // Fire only if ammo is not 0
        if (ammo != 0)
        {
            // Account for spread by generating random angle
            float curRot = bulletSpawn.rotation.eulerAngles.z;
            Quaternion bulletAngle = Quaternion.Euler(new Vector3(0, 0, curRot + Random.Range(-spread / 2, spread / 2)));
            // Create bullet
            GameObject bullet = ObjectPoolManager.SharedInstance.GetPooledObject(shellType.name);
            if (bullet != null)
            {
                bullet.transform.position = bulletSpawn.transform.position;
                bullet.transform.rotation = bulletAngle;
                bullet.SetActive(true);
                Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
                // Apply speed and power buff if bullet is just created (i.e. not recovered from object pool)
                float bulletSpeed = bullet.GetComponent<Bullet>().speed * (1 + speedBuff);
                bullet.GetComponent<Bullet>().SetCurSpeed(bulletSpeed);
                bullet.GetComponent<Bullet>().SetCurPower(bullet.GetComponent<Bullet>().power * (1 + powerBuff));
                // Push bullet
                rig.velocity = bulletSpawn.up * bulletSpeed;
                // Recoil
                Rigidbody2D parent = transform.parent.GetComponent<Rigidbody2D>();
                if (parent != null)
                {
                    parent.AddForce(-recoilForce * bulletSpawn.up, ForceMode2D.Force);
                }
                // Deplete ammo if not unlimited
                if (ammo > 0)
                {
                    ammo--;
                }
            }
        }
    }
}
