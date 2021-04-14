using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Ground_Phase_Camera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam1, //PHASES 3 & 4
        vcam2, vcam3;

    int prior;

    [SerializeField]
    private Transform[] Path;
   
    int path;

    private int routeToGo;

    private float tParam;

    private Vector2 AirPosition;

    public float speed; // 6 to 9

    public float P3_speed;

    public float P4_speed;

    public float timer = 0;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
        speed = P3_speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        prior = vcam2.Priority;


        if (vcam3.Priority == 1 || vcam2.Priority == 1)
        {
            if (coroutineAllowed)
            {
                StartCoroutine(GoByTheRoute(routeToGo));
            }
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {

        coroutineAllowed = false;


       //HONG: if statements for the speed of the curves.


        if (path == 1 && routeNumber == 0){
            speed = P4_speed;
            }
        if (path == 1 && routeNumber == 1)
        {
            speed = .088f;
        }
        //Path --> route --> position point.  what this code is doing

        Vector2 p0 = Path[path].GetChild(routeNumber).GetChild(0).position;
        Vector2 p1 = Path[path].GetChild(routeNumber).GetChild(1).position;
        Vector2 p2 = Path[path].GetChild(routeNumber).GetChild(2).position;
        Vector2 p3 = Path[path].GetChild(routeNumber).GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;   /// requires some form of speed variable

            Vector2 lastPosition = transform.position;

            AirPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                       3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                       3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                        Mathf.Pow(tParam, 3) * p3;

            transform.position = AirPosition;
            
            yield return new WaitForEndOfFrame();
        }

        tParam = 0;

        routeToGo += 1;

        Debug.Log("Path: "+path+ " Route: "+routeNumber+ " Time: "+timer);

        if (routeToGo > Path[path].childCount - 1)
        { ///was routes.Length still counts the number of routes
            routeToGo = 0;
            path++;
            vcam2.Priority = 0;
            vcam3.Priority = 1;
        }

        coroutineAllowed = true;

        if (path > 1)
        {
            vcam3.Priority = 0;
            coroutineAllowed = false;
        }else{
            coroutineAllowed = true;
        }

    }

}
