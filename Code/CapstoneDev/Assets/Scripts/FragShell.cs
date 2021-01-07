using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for Terrod's fragmentation shells
//TODO Finish and test
public class FragShell : MonoBehaviour, IEnemyProjectile
{

     public int numFragments = 8;
     public bool fixedSpread = true;
     public float spin = 45f;
     public Rigidbody2D rb; //May not be needed?

     float curAngle = 0f;
     float angle = 0f;

     //Checking to see when the GameObject should be destroyed
     public void FixedUpdate()
     {
          //For when it is time for this object to be destroyed
          //Fracture();
     }
     
     //Here to allow for the mathmatics to be implemented
     public void Fracture()
     {
          //TODO Look into Unity Engine's Random Class
          System.Random rand = new System.Random();

          for (int i = 0; i < numFragments; i++)
          {
               //May need to change the transform.up.x into something else
               if(fixedSpread && numFragments > 0)
               {
                    curAngle = i * (360f / numFragments);
                    angle = transform.up.x + curAngle + spin;
                    //Fire Projectile at angle
               } else if(numFragments > 0)
               {
                    curAngle = rand.Next(361);
                    angle = transform.up.x + curAngle + spin;
                    //Fire Projectile at angle
               }
          }

          Destroy(gameObject);
     }

     public void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log(collision.name);
          //TODO add bullet damage and hit effect
          Player p = collision.GetComponent<Player>();
          if (p != null)
          {
               //p.TakeDamage(power);
               Destroy(gameObject);
          }
     }
}
