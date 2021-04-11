using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;//May use this

public class Narration : MonoBehaviour
{
     public Image portrait;
     public Text textBox;

     public LineSet startSet;
     int currentPriority;
     LineSet currentSet;
     int currentLine;
     float timer;

     // Start is called before the first frame update
     void Start()
     {
          timer = 0;
          currentSet = startSet;
          currentPriority = currentSet.priority;
          currentLine = 0;
          currentSet.SetIsActive(true);
     }

     // Update is called once per frame
     void Update()
     {
          timer += Time.deltaTime;
          if(currentLine < currentSet.allLines.Length && currentSet.allLines[currentLine].time <= timer)
          {
               //Play line
               portrait.sprite = currentSet.allLines[currentLine].sprite;
               textBox.text = currentSet.allLines[currentLine].textLine;
               currentLine++;
          }
     }
     
     public void ChangeLineSet(LineSet newSet)
     {
          if (newSet.priority > currentSet.priority)
          {
               timer = 0;
               currentLine = 0;
               currentSet.SetIsActive(false);
               currentSet = newSet;
               currentSet.SetIsActive(true);
               currentPriority = currentSet.priority;
          }
     }
}
