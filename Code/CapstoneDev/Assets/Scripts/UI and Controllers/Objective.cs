﻿using System.Collections;
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

     void Awake()
     {
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
     //Not currently used as a bug was occurring in the ObjectivesSystem script
     /*public ObjectiveStatus GetStatus()
     {
          return status;
     }*/
}
