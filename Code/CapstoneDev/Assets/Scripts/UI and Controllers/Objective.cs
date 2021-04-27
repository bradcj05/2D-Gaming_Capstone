using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective", menuName = "Objective/Objectives")]

[System.Serializable]
public class Objective : ScriptableObject
{
     public enum ObjectiveType
     {
          Destroy,
          Collect,
          Tower,
          Failproof,
          Impossible
     }

     public enum ObjectiveStatus
     {
          Inactive,
          Active,
          Completed,
          Failed,
          Cancelled
     }

     public ObjectiveType type;
     public ObjectiveStatus status;
     public string description;
     public GameObject target;
     public int reward;
     public int requiredAmount;
     protected int currentAmount;

     //To help with keeping objectives completed after retrying at a checkpoint
     protected static ObjectiveStatus resetStatus = ObjectiveStatus.Inactive;
     protected static int resetAmount = -1;

     void Awake()
     {
          status = ObjectiveStatus.Inactive;
          currentAmount = 0;
     }

     void CheckCompletion()
     {
          if(currentAmount >= requiredAmount && status == ObjectiveStatus.Active)
          {
               status = ObjectiveStatus.Completed;
               ScoreTextScript.coinAmount += reward;
          }
     }

     public void IncreaseCurrent()
     {
          if (status == ObjectiveStatus.Active)
          {
               currentAmount++;
               CheckCompletion();
          }
     }

     public int GetCurrentAmount()
     {
          return currentAmount;
     }

     public void CheckpointUpdate()
     {
          resetAmount = currentAmount;
     }

     public void ResetObjective()
     {
          status = resetStatus;

          if (resetAmount == -1)
               currentAmount = 0;
          else
               currentAmount = resetAmount;
     }
}
