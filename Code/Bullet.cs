using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public int damageValue;
     public GameObject impact;

     //TODO: Update to provide bullet functionality
     void OnTriggerEnter2D(Collider2D collision)
     {
          //collision.gameObject
          Debug.Log(collision.name);
          Enemy e = collision.GetComponent<Enemy>();
          if(e != null)
          {
               //Add function for an enemy to take damage
               //e.TakeDamage(damageValue);
          }

          Instantiate(impact, transform.position, transform.rotation);

          Destroy(gameObject);
     }

}
