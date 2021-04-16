using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Bound : MonoBehaviour
{
    public GameObject left;
    public  GameObject right;
    public GameObject up;
    public GameObject down;

    private Vector2[] points;
    // Start is called before the first frame update
    void Start()
    {
        //Screen.width;
       
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                left.transform.position = new Vector3(Screen.width/4, Screen.height/2, 0);
                EdgeCollider2D edge = left.AddComponent<EdgeCollider2D>();
                edge.points[0] = new Vector2(0, -Screen.height / 2);
                edge.points[1] = new Vector2(0, Screen.height / 2);
                //edge.points = points;
                edge.edgeRadius = 0.2f;
                edge.isTrigger = true;
            }

            if (i == 1)
            {
                right.transform.position = new Vector3(3*Screen.width / 4, Screen.height / 2, 0);
                EdgeCollider2D edge = right.AddComponent<EdgeCollider2D>();
                edge.points[0] = new Vector2(0, -Screen.height / 2);
                edge.points[1] = new Vector2(0, Screen.height / 2);
                //edge.points = points;
                edge.edgeRadius = 0.2f;
                edge.isTrigger = true;
            }

            if (i == 2)
            {
                up.transform.position = new Vector3(Screen.width /2, Screen.height, 0);
                EdgeCollider2D edge = up.AddComponent<EdgeCollider2D>();
                edge.points[0] = new Vector2(-Screen.width / 2, 0);
                edge.points[1] = new Vector2(Screen.width / 2, 0);
                //edge.points = points;
                edge.edgeRadius = 0.2f;
                edge.isTrigger = true;
            }

            if (i == 3)
            {
                down.transform.position = new Vector3(Screen.width / 2, 0, 0);
                EdgeCollider2D edge = down.AddComponent<EdgeCollider2D>();
                edge.points[0] = new Vector2(-Screen.width / 2, 0);
                edge.points[1] = new Vector2(Screen.width / 2, 0);
                //edge.points = points;
                edge.edgeRadius = 0.2f;
                edge.isTrigger = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
