using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSortingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = "Bullets";
        GetComponent<Renderer>().sortingOrder = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
