using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] path;
    public float bezierSpeed = 0.1f; // = 1 / (Time to finish a Bezier Curve)

    private int routeToGo;
    private float tParam;
    private Vector2 AirPosition;

    private bool coroutineAllowed;
    protected Rigidbody2D rb;

    // Params for optional features
    public bool repeating = false;
    public bool rotateTowardsTarget = true;
    public string[] targetTags;
    protected Transform target;
    protected float distanceToTarget = Mathf.Infinity;
    protected float rotateAmount;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
        if (rotateTowardsTarget)
            FindClosestTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateTowardsTarget)
            FindClosestTarget();
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }

    // Rotate towards playercode
    public void FindClosestTarget()
    {
        try
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            GameObject closest = null;
            foreach (string tag in targetTags)
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
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
            target = null;
            distanceToTarget = Mathf.Infinity;
        }
    }

    void FixedUpdate()
    {
        if (target != null && rotateTowardsTarget)
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            if (Vector3.Dot(direction, transform.up) <= 0)
            {
                rotateAmount = 1f;
            }
            else
            {
                rotateAmount = Vector3.Cross(direction, transform.up).z;
            }
            float curRot = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot - rotateSpeed * rotateAmount));
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        if (routeToGo <= path.Length - 1)
        {
            coroutineAllowed = false;

            Vector2 p0 = path[routeNumber].GetChild(0).position;
            Vector2 p1 = path[routeNumber].GetChild(1).position;
            Vector2 p2 = path[routeNumber].GetChild(2).position;
            Vector2 p3 = path[routeNumber].GetChild(3).position;

            while (tParam < 1)
            {
                tParam += Time.deltaTime * bezierSpeed;   /// requires some form of speed variable
                Vector2 lastPosition = transform.position;
                AirPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                           3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                           3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                            Mathf.Pow(tParam, 3) * p3;

                //transform.position = AirPosition;
                rb.velocity = AirPosition - lastPosition;
                yield return new WaitForEndOfFrame();
            }

            tParam = 0;

            routeToGo += 1;

            if (routeToGo > path.Length - 1 && repeating)
                routeToGo = 0;
            else
                rb.velocity = new Vector2(0, 0);

            coroutineAllowed = true;
        }
    }
}
