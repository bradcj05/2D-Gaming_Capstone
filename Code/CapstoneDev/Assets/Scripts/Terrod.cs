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
    protected float rotateAmount; //public for better testing
    protected Transform target;

    //Add death animation

    public void Start()
    {
        isWorking1 = true;
        isWorking2 = true;
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

    public void Update()
    {
        isWorking1 = track1.GetComponent<TerrodTracks>().TracksWorking();
        isWorking2 = track2.GetComponent<TerrodTracks>().TracksWorking();

        if (!isWorking1 || !isWorking2)
        {
            rig.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rig.constraints = RigidbodyConstraints2D.None;
        }

        if (turretRigMain == null && turretRig1 == null && turretRig2 == null && flak1 == null && flak2 == null)
        {
            //Death animation
            Destroy(gameObject);
        }
    }

    public void FixedUpdate()
    {
        //Changing Terrod's movement
        if (target != null && isWorking1 && isWorking2)
        {
            Vector2 direction = (Vector2)target.position - rig.position;
            direction.Normalize();
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
            rig.velocity = -transform.up * moveSpeed;
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
