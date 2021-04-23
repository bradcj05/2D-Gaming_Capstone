using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Bound : MonoBehaviour
{
    public GameObject left;
    public  GameObject right;
    public GameObject up;
    public GameObject down;

    Vector2[] points;
    // Start is called before the first frame update
    [SerializeField]
    private CinemachineVirtualCamera vcam1;

    void Start()
    {
        //Screen.width;
        float scale = vcam1.m_Lens.OrthographicSize/10;
        for (int i = 0; i < 4; i++)
        {
            points = new Vector2[2];

            if (i == 0)
            {
                left.transform.position *= scale;
                EdgeCollider2D edge = left.AddComponent<EdgeCollider2D>();
                points[0] = new Vector2(0, -15);
                points[1] = new Vector2(0, 15);
                edge.points = points;
                edge.edgeRadius = 0.2f;
                edge.isTrigger = true;
            }

            if (i == 1)
            {
                right.transform.position *= scale;
                EdgeCollider2D edge = right.AddComponent<EdgeCollider2D>();
                points[0] = new Vector2(0, -15);
                points[1] = new Vector2(0, 15);
                edge.points = points;
                edge.edgeRadius = 0.2f;
                edge.isTrigger = true;
            }

            if (i == 2)
            {
                up.transform.position *= scale;
                EdgeCollider2D edge = up.AddComponent<EdgeCollider2D>();
                points[0] = new Vector2(-15,0);
                points[1] = new Vector2(15,0);
                edge.points = points;
                edge.edgeRadius = 0.2f;
                edge.isTrigger = true;
            }

            if (i == 3)
            {
                down.transform.position *= scale;
                EdgeCollider2D edge = down.AddComponent<EdgeCollider2D>();
                points[0] = new Vector2(-15, 0);
                points[1] = new Vector2(15, 0);
                edge.points = points;
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
