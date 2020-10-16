using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
     public int damageValue;

     //Need to find out how to implement this.
     public int penetration;
     public float degregation;

     //TODO: Update to provide bullet functionality
     void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log(collision.name);
          //TODO add bullet damage and hit effect
          Player p = collision.GetComponent<Player>();
          if (p != null)
          {
               p.TakeDamage(damageValue);
          }
          Destroy(gameObject);
     }
}
