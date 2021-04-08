using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective : MonoBehaviour
{
     public enum ObjectiveType
     {
          Destroy,
          Collect
     }

     public ObjectiveType type;
     public string description;
     public GameObject target;
     public int reward;
     public int requiredAmount;
     public int currentAmount;

     public bool IsObjectiveComplete()
     {
          if (currentAmount >= requiredAmount)
               return true;
          else
               return false;
     }

     //public void 
}
