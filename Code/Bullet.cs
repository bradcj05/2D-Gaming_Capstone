using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public int damageValue;

     //TODO: Update to provide bullet functionality
     void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log(collision.name);
          //TODO add bullet damage and hit effect

          Destroy(gameObject);
     }

}
