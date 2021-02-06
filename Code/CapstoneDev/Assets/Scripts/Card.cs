using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName ="Cards/PlayerPlanes")]
//This will be a template frr the data to store.

public class Card : ScriptableObject{
     public int cardType; // 1 for plane, 2 for gun, 3 for shell
     public GameObject obj;
    public new string name;
    public int cost;
    public Sprite artwork;

    // Plane values
    public string speed;
    public string defense;
    public string health;

     // Gun values
     public string reloadTime;
     public string powerBuff;
     public string speedBuff;

     // Shell values
     public string power;
     public string shellSpeed;
     public string penetration;
     public string deterioration;
}
