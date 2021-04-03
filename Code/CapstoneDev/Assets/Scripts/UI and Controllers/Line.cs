using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Narration Line", menuName = "Narration/Line")]

public class Line : ScriptableObject
{
     public Sprite portrait;
     public string text;
     public float trigger;

     void Awake()
     {
        
     }
}
