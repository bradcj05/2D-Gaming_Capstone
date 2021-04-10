using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;//May use this

public class Narration : MonoBehaviour
{
     public Image portrait;
     public Text textBox;
     
     public LineSet[] lines; //Current format for testing has 0 for "* seconds have passed", 1 for boss defeated text, and 2 for plane destroyed text
     int currentSet;
     int currentLine;
     float timer;

     // Start is called before the first frame update
     void Start()
     {
          timer = 0;
          currentSet = 0;
          currentLine = 0;
          lines[currentSet].SetIsActive(true);
     }

     // Update is called once per frame
     void Update()
     {
          timer += Time.deltaTime;
          if(currentLine < lines[currentSet].allLines.Length && lines[currentSet].allLines[currentLine].time <= timer)
          {
               //Play line
               portrait.sprite = lines[currentSet].allLines[currentLine].sprite;
               textBox.text = lines[currentSet].allLines[currentLine].textLine;
               currentLine++;
          }
     }
     
     public void ChangeLineSet(int setNumber)
     {
          timer = 0;
          currentLine = 0;
          lines[currentSet].SetIsActive(false);
          currentSet = setNumber;
          lines[currentSet].SetIsActive(true);
     }
}
