using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airos : Enemy /// Always include "Enemy" and "Die()" function
{
    //Movement Variables
    
    public Rigidbody2D rig;
    public Rigidbody2D turretL1;
    public Rigidbody2D turretR2;
    //Vector2 movement;

   // public float moveSpeed = 5f;
    public float rotateSpeed = 0.01f;
    protected float rotateAmount; //public for better testing
    protected Transform target;

    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 AirPosition;
    public float speed;
    private bool coroutineAllowed;

    new void Start()
    {

        routeToGo = 0;
        tParam = 0f;
        // speed = 0.5f;
        coroutineAllowed = true;

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

    new void Update() //// check on this HERE!!!!!!!!
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }           //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    void FixedUpdate()
    {
        if (target != null)
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
           // rig.velocity = -transform.up * moveSpeed;
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

    private IEnumerator GoByTheRoute(int routeNumber)
    {

        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;   /// requires some form of speed variable

            AirPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                       3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                       3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                        Mathf.Pow(tParam, 3) * p3;

            transform.position = AirPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        coroutineAllowed = true;

    }

}


