/** Script for the Black Condor as he appears in Level 1
 * The Black Condor will attempt to evade incoming bullets
 * and other objects defined by the "tagsToAvoid" field.
 * It strafes left or right to avoid the object,
 * and strafes slowly or quickly depending on how far the object is.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCondorLv1 : Enemy
{
    public string[] tagsToAvoid = { "ActivePlayer", "Ally" };
    protected Transform target;
    protected Collider2D collider;
    protected float distanceToTarget;
    protected Vector3 directionToTarget;
    protected float originalRotation = 0;
    public float scanRadius = 20f; // If a bullet is inside this radius, attempts to avoid
    public float maxSpeed = 12f;
    public float accelSpeed = 15f;
    public float decelSpeed = 15f;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        collider = gameObject.GetComponent<Collider2D>();
        originalRotation = transform.localEulerAngles.z;
        // May have to change player target to something else for allies
        FindClosestTarget();
    }

    public void OnEnable()
    {
        FindClosestTarget();
    }

    // Function to find closest non-bullet object to avoid across EVERY tag
    public void FindClosestTarget()
    {
        try
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            GameObject closest = null;
            foreach (string tag in tagsToAvoid)
            {
                GameObject[] gos;
                gos = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject go in gos)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
            }
            target = closest.transform;
            distanceToTarget = Mathf.Sqrt(distance);
            directionToTarget = (target.position - transform.position).normalized;
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
            target = null;
            distanceToTarget = Mathf.Infinity;
            directionToTarget = new Vector3(0, 1, 0);
        }
    }

    // Function to find closest allied bullets
    public void FindClosestBullet()
    {
        LayerMask mask = LayerMask.GetMask("Player Bullets");
        Collider2D[] bulletsInSight = Physics2D.OverlapCircleAll(transform.position, scanRadius, mask);
        foreach (Collider2D bullet in bulletsInSight)
        {
            float distanceToBullet = collider.Distance(bullet).distance;
            if (distanceToBullet < distanceToTarget)
            {
                distanceToTarget = distanceToBullet;
                directionToTarget = -collider.Distance(bullet).normal;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        FindClosestBullet();
    }

    // Avoidant strafing movement
    void FixedUpdate()
    {
        if (0 < distanceToTarget && distanceToTarget <= scanRadius)
        {
            if (Vector3.Cross(transform.up, directionToTarget).z >= 0)
            {
                rb.velocity = transform.right * maxSpeed * Mathf.Min(1, scanRadius / (distanceToTarget * 10f));
            }
            else
            {
                rb.velocity = -transform.right * maxSpeed * Mathf.Min(1, scanRadius / (distanceToTarget * 10f));
            }
        }
    }
}
