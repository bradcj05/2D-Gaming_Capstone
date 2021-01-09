using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IEnemyProjectile
{
     public float power;
     public float speed;
     public float penetration;
     public float deterioration; //ratio/second

     //For Matt's explosion animation
     public GameObject explosion;

     //TODO: Update to provide bullet functionality
     public void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log(collision.name);
          //TODO add bullet damage and hit effect
          Player p = collision.GetComponent<Player>();
          FragShell fs = gameObject.GetComponent<FragShell>();
          if (p != null)
          {
               p.TakeDamage(power);

               if (fs != null)
                    fs.Fracture();

               Destroy(gameObject);
          }
     }
}
