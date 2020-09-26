using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

     //TODO: Update to provide bullet functionality
     void OnTriggerEnter2D(Collider2D collision)
     {
          //collision.gameObject
          Debug.Log(collision.name);
          Destroy(gameObject);
     }

}
