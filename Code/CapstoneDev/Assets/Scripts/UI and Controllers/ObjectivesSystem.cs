using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesSystem : MonoBehaviour
{
     public Text objectiveBox;
     public Objective[] objectives;

     void Awake()
     {
          for (int i = 0; i <objectives.Length; i++)
          {
               objectives[i].status = Objective.ObjectiveStatus.Inactive;
          }
     }

     // Update is called once per frame
     void Update()
     {
          string boxText = "";
          for (int i = 0; i < objectives.Length; i++)
          {
               if (objectives[i].status == Objective.ObjectiveStatus.Active)
               {
                    if (objectives[i].requiredAmount > 0)
                    {
                         boxText += objectives[i].description + ": " +
                              objectives[i].GetCurrentAmount() + "/" + objectives[i].requiredAmount;
                    }
                    else
                    {
                         boxText += objectives[i].description;
                    }
                    if ((i + 1) < objectives.Length)
                         boxText += "\n";
               }
          }
          objectiveBox.text = boxText;
     }
     
     public void DestroyUpdate(string destroyedName)
     {
          for (int i = 0; i < objectives.Length; i++)
          {
               if (objectives[i].status == Objective.ObjectiveStatus.Active && 
                 objectives[i].type == Objective.ObjectiveType.Destroy && 
                 string.Equals(objectives[i].target.name, destroyedName))
                    objectives[i].IncreaseCurrent();
          }
     }

     public void CollectUpdate()
     {
          for(int i = 0; i < objectives.Length; i++)
          {
               if (objectives[i].status == Objective.ObjectiveStatus.Active &&
                 objectives[i].type == Objective.ObjectiveType.Collect)
                    objectives[i].IncreaseCurrent();
          }
     }
     
     public void TowerUpdate()
     {
          for(int i = 0; i < objectives.Length; i++)
          {
               if (objectives[i].status == Objective.ObjectiveStatus.Active && objectives[i].type == Objective.ObjectiveType.Tower)
                    objectives[i].IncreaseCurrent();
          }
     }
 
     public void ActivateObjectives(int obj1, int obj2)
     {
          if (objectives[obj1].status == Objective.ObjectiveStatus.Inactive)
               objectives[obj1].status = Objective.ObjectiveStatus.Active;

          if (obj2 != -1)
          {
               if (objectives[obj2].status == Objective.ObjectiveStatus.Inactive)
                    objectives[obj2].status = Objective.ObjectiveStatus.Active;
          }
     }
     
     public void CompleteAutomatic(int obj1, int obj2)
     {
          if (objectives[obj1].type == Objective.ObjectiveType.Failproof)
          {
               objectives[obj1].status = Objective.ObjectiveStatus.Completed;
               ScoreTextScript.coinAmount += objectives[obj1].reward;
          }
          else if (objectives[obj1].type == Objective.ObjectiveType.Impossible)
          {
               objectives[obj1].status = Objective.ObjectiveStatus.Cancelled;
          }
          else if (objectives[obj1].status == Objective.ObjectiveStatus.Active)
          {
               objectives[obj1].status = Objective.ObjectiveStatus.Failed;
          }

          if (obj2 != -1)
          {
               if (objectives[obj2].type == Objective.ObjectiveType.Failproof)
               {
                    objectives[obj2].status = Objective.ObjectiveStatus.Completed;
                    ScoreTextScript.coinAmount += objectives[obj2].reward;
               }
               else if (objectives[obj2].type == Objective.ObjectiveType.Impossible)
               {
                    objectives[obj2].status = Objective.ObjectiveStatus.Cancelled;
               }
               else if (objectives[obj1].status == Objective.ObjectiveStatus.Active)
               {
                    objectives[obj2].status = Objective.ObjectiveStatus.Failed;
               }
          }
     }
}
