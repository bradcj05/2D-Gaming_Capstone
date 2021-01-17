using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName ="Cards/PlayerPlanes")]
//This will be a template frr the data to store.

public class Card : ScriptableObject{
    public new string name;
    public string cost;
    public Sprite artwork;



}
