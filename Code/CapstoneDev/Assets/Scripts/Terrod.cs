using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrod : MonoBehaviour
{
    //Components to function as a boss
    public Rigidbody2D rig;
    public Rigidbody2D turretRig1;
    public Rigidbody2D turretRig2;
    public Rigidbody2D turretRigMain;
    public Rigidbody2D flak1;
    public Rigidbody2D flak2;

    //Keep track of the Track objects so that movement is done right.
    public GameObject track1;
    public GameObject track2;
    bool isWorking1;
    bool isWorking2;

    //Movement
    public float moveSpeed = 0.5f;
    public float rotateSpeed = 0.01f;
    public float reverseSpeed = 0.35f;
    public float optimumDistance = 15f; // Optimum distance to player before stopping
    public float accelTime = 1f;  // Acceleration time (until maximum engine power in either direction)
    public float decelTime = 0.5f;  // Deceleration time
    protected float rotateAmount; //public for better testing
    protected Transform target;
    bool reverse = false; // Check if reversing

    //Add death animation

    public void Start()
    {
        isWorking1 = true;
        isWorking2 = true;
        try
        {
            target = GameObject.FindGameObjectWithTag("ActivePlayer").transform;
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
            target = null;
        }
    }

    public void Update()
    {
        //Try to find the next player plane when it spawns
        try
        {
            target = GameObject.FindGameObjectWithTag("ActivePlayer").transform;
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
            target = null;
        }
        // Track behavior
        isWorking1 = track1.GetComponent<TerrodTracks>().TracksWorking();
        isWorking2 = track2.GetComponent<TerrodTracks>().TracksWorking();
        // Stop movement when a track is not working, else activate it
        if (!isWorking1 || !isWorking2)
        {
            rig.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rig.constraints = RigidbodyConstraints2D.None;
        }
        // "Death" upon destruction of all mounted weapons
        if (turretRigMain == null && turretRig1 == null && turretRig2 == null && flak1 == null && flak2 == null)
        {
               //Set the correct progression bool to true
               Progression.progress[1] = true;
            //Death animation
            Destroy(gameObject);
        }
    }

    public void FixedUpdate()
    {
        //Changing Terrod's movement
        if (target != null && isWorking1 && isWorking2)
        {
            Vector2 distance = (Vector2)target.position - rig.position;
            reverse = Vector2.Dot(distance, -transform.up) < 0;
            // Move forward only if distance to player is larger than "optimum" distance and player is in front
            if (distance.magnitude > optimumDistance && !reverse)
            {
                Run();
            } 
            // Else reverse, but again only if closer than optimum distance
            else if (distance.magnitude > optimumDistance)
            {
                Reverse();
            }
            // Else simply rotate towards player
            else
            {
                Stop();
                Rotate();
            }

        }
    }

    // Custom rotation (ASSUME SPRITE FACING DOWNWARD!!!)
    public void Rotate()
    {
        // Rotate
        Vector2 direction = ((Vector2)target.position - rig.position).normalized;
        if (Vector3.Dot(direction, -transform.up) <= 0)
        {
            rotateAmount = 1f;
        }
        else
        {
            rotateAmount = Vector3.Cross(direction, -transform.up).z;
        }
        float curRot = transform.rotation.eulerAngles.z;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot - rotateSpeed * rotateAmount));
    }

    // Custom acceleration and deceleration (ASSUME SPRITE FACING DOWNWARD!!!)
    public void Run()
    { 
        // If reversing, stop first
        if (reverse)
        {
            Stop();
        }
        // Rotate when running
        Rotate();
        // Acceleration
        if (rig.velocity.magnitude < moveSpeed)
        {
            rig.velocity = -transform.up * (rig.velocity.magnitude + moveSpeed * Time.deltaTime / accelTime);
        }
        // Velocity capping
        if (rig.velocity.magnitude > moveSpeed)
        {
            rig.velocity = -transform.up * moveSpeed;
        }
    }

    public void Stop()
    {
        // Deceleration
        Vector2 prevVelocity = new Vector2(rig.velocity.x, rig.velocity.y);
        if (rig.velocity.magnitude > 0)
        {
            if (reverse)
            {
                rig.velocity = prevVelocity - prevVelocity.normalized * (reverseSpeed * Time.deltaTime / accelTime);
            }
            else
            {
                rig.velocity = prevVelocity - prevVelocity.normalized * (moveSpeed * Time.deltaTime / accelTime);
            }
        }
        // Velocity capping, also rotate when "completely" stopped
        if (Vector2.Dot(rig.velocity, prevVelocity) < 0)
        {
            rig.velocity = new Vector2(0,0);
        }
        // Rotate when tank achieves "stability" (active engine power = 10% total engine power)
        if ((!reverse && rig.velocity.magnitude < moveSpeed / 10) || (reverse && rig.velocity.magnitude < reverseSpeed / 10)) 
        {
            Rotate();
        }
    }

    public void Reverse()
    {
        // Stop first if still going forward
        if (!reverse)
        {
            Stop();
        }
        else
        {
            // Also rotate
            Rotate();
            // Reverse until reverseSpeed
            if (rig.velocity.magnitude < reverseSpeed)
            {
                rig.velocity = transform.up * (rig.velocity.magnitude + reverseSpeed * Time.deltaTime / accelTime);
            }
            if (rig.velocity.magnitude > reverseSpeed)
            {
                rig.velocity = transform.up * reverseSpeed;
            }
        }
    }
}
