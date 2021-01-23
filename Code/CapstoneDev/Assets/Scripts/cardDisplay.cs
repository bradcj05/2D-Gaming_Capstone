using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cardDisplay : MonoBehaviour
{

    public Card cards;

    //attributes represent Health, defense, speed under Attributes canvas
    public GameObject attributes;
    

   

    public Image artwork;
    public Text nameText;
    public Text costText;

    public Text speedText;
    public Text healthText;
    public Text defenseText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = cards.name;
        costText.text = cards.cost;
        artwork.sprite = cards.artwork;

        speedText.text = cards.speed;
        healthText.text = cards.health;
        defenseText.text = cards.defense;



    }

   


}
