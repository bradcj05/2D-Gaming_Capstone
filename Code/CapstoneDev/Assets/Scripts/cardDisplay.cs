using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cardDisplay : MonoBehaviour
{
    public GameObject attributes;
  

    public Card cards;

    public Image artwork;
    public Text nameText;
    public Text costText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = cards.name;
        costText.text = cards.cost;
        artwork.sprite = cards.artwork;
    }

   


}
