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

    public Text t1;
    public Text t2;
    public Text t3;

    // Start is called before the first frame update
    //TODO Finish
    void Start()
    {
        nameText.text = cards.name;
        costText.text = cards.cost.ToString();
        artwork.sprite = cards.artwork;

          switch (cards.cardType)
          {
               case 1:
                    t1.text = cards.speed;
                    t2.text = cards.health;
                    t3.text = cards.defense;
                    break;
               case 2:
                    t1.text = cards.reloadTime;
                    t2.text = cards.powerBuff;
                    t3.text = cards.speedBuff;
                    break;
               case 3:
                    t1.text = cards.power;
                    t2.text = cards.shellSpeed;
                    t3.text = cards.penetration;
                    break;
               default:
                    Debug.Log("Please give this card the right card type.");
                    break;
          }
    }

     // May not be the right script for this function
     public void BuyItem()
     {
          if (ScoreTextScript.coinAmount >= cards.cost)
          {
               bool purchased = false;

               // Add gameObject to the appropriate list of available objects
               // TODO Fix, something wrong with foreach loop
               switch (cards.cardType)
               {
                    case 1:
                         foreach (GameObject plane in ObjectList.planeList)
                         {
                              if(cards.obj == plane)
                              {
                                   Debug.Log("This item was already purchased.");
                                   purchased = true;
                                   break;
                              }
                         }
                         if (!purchased)
                         {
                              ObjectList.planeList.Add(cards.obj);
                              ScoreTextScript.coinAmount -= cards.cost;
                         }
                         break;
                    case 2:
                         foreach (GameObject gun in ObjectList.gunList)
                         {
                              if (cards.obj == gun)
                              {
                                   Debug.Log("This item was already purchased.");
                                   purchased = true;
                                   break;
                              }
                         }
                         if (!purchased)
                         {
                              ObjectList.gunList.Add(cards.obj);
                              ScoreTextScript.coinAmount -= cards.cost;
                         }
                         break;
                    case 3:
                         foreach (GameObject shell in ObjectList.shellList)
                         {
                              if (cards.obj == shell)
                              {
                                   Debug.Log("This item was already purchased.");
                                   purchased = true;
                                   break;
                              }
                         }
                         if (!purchased)
                         {
                              ObjectList.shellList.Add(cards.obj);
                              ScoreTextScript.coinAmount -= cards.cost;
                         }
                         break;
                    default:
                         Debug.Log("Please give this card the right card type.");
                         break;
               }
          }
          else
          {
               Debug.Log("Not enough coins.");
          }
     }
}
