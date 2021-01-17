using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Finish and Test
public class Turret : Gun
{
    public float rotateSpeed = 1f;
    protected float rotateAmount; //public for better testing
    public Transform target;
    protected Rigidbody2D rb;

    // Initialize rigid body
    public new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public new void Update() {
        base.Update();
    }

    public void FixedUpdate()
    {
        if (target != null)
        {
            // Homing missile code for aiming
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            if (Vector3.Dot(direction, transform.up) <= 0)
            {
                rotateAmount = 1;
            }
            else
            {
                rotateAmount = Vector3.Cross(direction, transform.up).z;
            }
            float curRot = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot - rotateSpeed * rotateAmount));
        }
        else
        {
            //Try to find the next player plane when it spawns
            try
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (System.NullReferenceException e)
            {
                Debug.Log(e);
                target = null;
            }
        }
    }
}
