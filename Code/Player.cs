using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

     public float moveSpeed = 5f;
     public Rigidbody2D rig;
     Vector2 movement;

    // Update is called once per frame
    //Input
    void Update()
    {
          movement.x = Input.GetAxisRaw("Horizontal");
          movement.y = Input.GetAxisRaw("Vertical");
    }

     //Movement
     void FixedUpdate()
     {
          rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
     }
}
