using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesSystem : MonoBehaviour
{
     Text objectiveBox;
     Objective[] objectives;

     // Update is called once per frame
     void Update()
     {
          string boxText = "";
          for (int i = 0; i < objectives.Length; i++)
          {
               boxText += objectives[i].description + ": " + 
                    objectives[i].currentAmount + "/" + objectives[i].requiredAmount;
               if ((i + 1) < objectives.Length)
                    boxText += "\n";
          }
          objectiveBox.text = boxText;
     }
}
