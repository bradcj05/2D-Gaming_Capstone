using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;//May use this

public class Narration : MonoBehaviour
{
     public Image portrait;
     public Text textBox;

     public Sprite[] sprites;
     public string[] lines;
     public int[] spriteToUse;
     public float[] triggers;
     int currentLine;
     float timer;



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
          if(currentLine < lines.Length && triggers[currentLine] <= timer)
          {
               //Play line
               portrait.sprite = sprites[spriteToUse[currentLine]];
               textBox.text = lines[currentLine];
               currentLine++;
          }
     }

     public void ChangeText(string l, int s)
     {
          portrait.sprite = sprites[s];
          textBox.text = l;
     }
}
