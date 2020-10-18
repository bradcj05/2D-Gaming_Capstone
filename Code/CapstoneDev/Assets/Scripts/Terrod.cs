using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrod : MonoBehaviour
{
     public float moveSpeed = 5f;
     public Rigidbody2D rig;
     public Rigidbody2D turretRig1;
     public Rigidbody2D turretRig2;
     public Rigidbody2D turretRigMain;
     Vector2 movement;

     //Add death animation
     //Allow for rotation
     //Add AI  
     
     void Update()
     {
          movement.x = Input.GetAxisRaw("Horizontal");
          movement.y = Input.GetAxisRaw("Vertical");

          if (turretRigMain == null)
          {
               //Death animation
               Destroy(gameObject);
          }
          
     }

     void FixedUpdate()
     {
          rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
          turretRig1.MovePosition(turretRig1.position + movement * moveSpeed * Time.fixedDeltaTime);
          turretRig2.MovePosition(turretRig2.position + movement * moveSpeed * Time.fixedDeltaTime);
          turretRigMain.MovePosition(turretRigMain.position + movement * moveSpeed * Time.fixedDeltaTime);
     }
}
