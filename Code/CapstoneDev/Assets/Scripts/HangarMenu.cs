using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangarMenu : MonoBehaviour
{
     //May not need this class in the future
     int index;
     int selectionIndex;

     public GameObject AS57;
     public GameObject P62;
     public GameObject gunR51T;
     public GameObject shellR5AP;

     GameObject selectedPlane;
     GameObject selectedGun;
     GameObject selectedShell;

     void Awake()
     {
          index = 0;
          selectionIndex = 0;
     }

     public void SelectOption()
     {
          //Select Option and move to next part
          //Need to properly implement part selection
          switch (selectionIndex)
          {
               case 0:
                    if(AS57.GetComponent<SpriteRenderer>() == true)
                    {
                         selectedPlane = AS57;
                    }
                    else
                    {
                         selectedPlane = P62;
                    }
                    AS57.GetComponent<SpriteRenderer>().enabled = false;
                    P62.GetComponent<SpriteRenderer>().enabled = false;
                    gunR51T.GetComponent<SpriteRenderer>().enabled = true;
                    selectionIndex++;
                    index = 0;
                    break;
               case 1:
                    selectedGun = gunR51T;
                    gunR51T.GetComponent<SpriteRenderer>().enabled = false;
                    shellR5AP.GetComponent<SpriteRenderer>().enabled = true;
                    selectionIndex++;
                    index = 0;
                    break;
               case 2:
                    selectedShell = shellR5AP;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
               default:
                    break;
          }
     }

     public void NextOption()
     {
          //Set Current image to not active
          //index++;
          //Set next image to active

          //Temporary Solution
          if(selectionIndex == 0)
          {
               AS57.GetComponent<SpriteRenderer>().enabled = false;
               P62.GetComponent<SpriteRenderer>().enabled = true;
          }
     }

     public void PreviousOption()
     {
          //Set Current image to not active
          //index--;
          //Set next image to active

          //Temporary Solution
          if (selectionIndex == 0)
          {
               AS57.GetComponent<SpriteRenderer>().enabled = true;
               P62.GetComponent<SpriteRenderer>().enabled = false;
          }
     }

     //May or May not need this function
     void LoadPlane(GameObject plane, GameObject gun, GameObject shell)
     {

     }
}
