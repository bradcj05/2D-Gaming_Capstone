using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEvents : MonoBehaviour
{
     //All work in progress, may not need this script in the future.
     public GameObject player;
     public GameObject gameOverMenu;

     public void Start()
     {

     }

     public void Update()
     {
          if(player == null)
          {
               gameOverMenu.SetActive(true);
          }
     }


}
