using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{

     public Transform gun;
     public GameObject shellType;
     public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
          if (Input.GetButtonDown("Fire1"))
          {
               Fire();
          }
    }

     public void Fire()
     {
          GameObject bullet = Instantiate(shellType, gun.position, gun.rotation);
          Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
          rig.AddForce(gun.up * speed, ForceMode2D.Impulse);
     }
}
