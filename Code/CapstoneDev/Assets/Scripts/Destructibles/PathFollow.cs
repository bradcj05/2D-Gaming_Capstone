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

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }

    private IEnumerator GoByTheRoute(int routeNumber)
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

        if (routeToGo > path.Length - 1)
            routeToGo = 0;

        coroutineAllowed = true;

    }
}
