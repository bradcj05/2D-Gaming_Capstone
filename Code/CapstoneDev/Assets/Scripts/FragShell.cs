using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for Terrod's fragmentation shells
//TODO Finish and TEST
public class FragShell : MonoBehaviour
{
     public int numFragments = 8;
     public bool fixedSpread = true;
     public float spin = 0f;
     public GameObject fragProjectile; //The fragmentation created
     public float bulletSpeed;
     public Rigidbody2D rb; //May not be needed?

     float curAngle = 0f;
     float angle = 0f;
     
     //Here to allow for the mathmatics to be implemented
     public void Fracture()
     {
          System.Random rand = new System.Random();

          for (int i = 0; i < numFragments; i++)
          {
               //May need to change the transform.up.x into something else
               if(fixedSpread && numFragments > 0)
               {
                    curAngle = i * (360f / numFragments);
                    angle = transform.up.x + curAngle + spin;
                    //Fire Projectile at angle; Test and FIX
                    //Implements the rotation math from the HM
                    GameObject bullet = Instantiate(fragProjectile, transform.position, rb.transform.rotation);
                    //Now rotate
                    float curRot = transform.localRotation.eulerAngles.z;
                    bullet.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot - angle));
                    //Propel forward
                    Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
                    rig.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
               } else if(numFragments > 0)
               {
                    curAngle = rand.Next(361);
                    angle = transform.up.x + curAngle + spin;
                    //Fire Projectile at angle; Test with fixed spread first
                    GameObject bullet = Instantiate(fragProjectile, transform.position, rb.transform.rotation);
                    //Now rotate
                    float curRot = transform.localRotation.eulerAngles.z;
                    bullet.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot - angle));
                    //Propel forward
                    Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
                    rig.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
               }
          }
     }

     void OnDisable()
     {
          if(this.enabled)
               Fracture();
     }
}
