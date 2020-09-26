using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

     //TODO: Update to provide bullet functionality
     void OnCollisionEnter2D(Collision2D collision)
     {
          //collision.gameObject
          Destroy(gameObject);
     }

}
