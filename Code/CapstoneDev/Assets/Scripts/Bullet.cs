using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public float power;
     public float speed;
     public float penetration;
     public float deterioration; //ratio/second
     float time = 0;

     void Update()
     {
          time += Time.deltaTime;
     }

     void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log(collision.name);
          //TODO add hit effect
          Enemy e = collision.GetComponent<Enemy>();
          if(e != null)
          {
               //Fix
               e.TakeDamage(power);
               //e.TakeDamage(power * Math.Pow(1 - deterioration * time, 2) - Math.Max(e.defense - penetration, 0));
               Destroy(gameObject);
          }
     }

}