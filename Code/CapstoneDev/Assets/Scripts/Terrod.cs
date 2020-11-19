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
     public GameObject flak1;
     public GameObject flak2;
     Vector2 movement;

     //Add death animation
     //Refine AI & add rotation

     void Start()
     {
          movement.y = -0.5f;
     }
     
     void Update()
     {
          if(movement.y < 0 && rig.position.y <= -6.5)
          {
               movement.y = 0.5f;
          }
          else if(movement.y > 0 && rig.position.y >= 6.5)
          {
               movement.y = -0.5f;
          }

          

          if (turretRigMain == null && turretRig1 == null && turretRig2 == null && flak1 == null && flak2 == null)
          {
               //Death animation
               Destroy(gameObject);
          }
     }
     
     void FixedUpdate()
     {
          rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
          if(turretRigMain != null)
               turretRigMain.MovePosition(turretRigMain.position + movement * moveSpeed * Time.fixedDeltaTime);
          if(turretRig1)
               turretRig1.MovePosition(turretRig1.position + movement * moveSpeed * Time.fixedDeltaTime);
          if(turretRig2)
               turretRig2.MovePosition(turretRig2.position + movement * moveSpeed * Time.fixedDeltaTime);
     }
     
}
