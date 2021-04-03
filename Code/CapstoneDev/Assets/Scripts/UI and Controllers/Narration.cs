using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Narration : MonoBehaviour
{
     public Image portrait;
     public Text textBox;

     public Line[] lines;
     int currentLine;
     public float timer;//public for testing

     // Start is called before the first frame update
     void Start()
     {
          timer = 0;
          currentLine = 0;
     }

     // Update is called once per frame
     void Update()
     {
          timer += Time.deltaTime;
          if(currentLine < lines.Length && lines[currentLine].trigger <= timer)
          {
               //Play line
               portrait.sprite = lines[currentLine].portrait;
               textBox.text = lines[currentLine].text;
               currentLine++;
          }
     }
}
