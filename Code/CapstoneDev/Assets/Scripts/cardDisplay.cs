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

     //For Stats bars
     public Slider s1;
     public Text st1;
     public Slider s2;
     public Text st2;
     public Slider s3;
     public Text st3;
     public Slider s4;
     public Text st4;

     // Start is called before the first frame update
     //TODO Finish
     void Start()
    {
          nameText.text = cards.name;
          costText.text = cards.cost.ToString();

          if (cards.unlock == -1 || Progression.progress[cards.unlock])
               artwork.sprite = cards.artwork;
          else
               gameObject.SetActive(false);

          switch (cards.cardType)
          {
               case 1:
                    t1.text = "Speed: " + cards.speed;
                    t2.text = "Health: " + cards.health;
                    t3.text = "Defense: " + cards.defense;
                    break;
               case 2:
                    t1.text = "Reload Time: " + cards.reloadTime;
                    t2.text = "Power Buff: " + cards.powerBuff;
                    t3.text = "Speed Buff: " + cards.speedBuff;
                    break;
               case 3:
                    t1.text = "Power: " + cards.power;
                    t2.text = "Speed: " + cards.shellSpeed;
                    t3.text = "Penetration: " + cards.penetration;
                    break;
               default:
                    Debug.Log("Please give this card the right card type.");
                    break;
          }
    }

     void Update()
     {
          if (cards.unlock == -1 || Progression.progress[cards.unlock])
               artwork.sprite = cards.artwork;
          else
               gameObject.SetActive(false);
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

     // Updates the stats bars at the top of the Hangar/Shop
     public void UpdateStats()
     {
          //There is propably a better way to do this, but I don't know right now
          switch (cards.cardType)
          {
               case 1:
                    s1.value = float.Parse(cards.speed) * 0.1f;
                    st1.text = "SPEED: " + cards.speed;
                    s2.value = float.Parse(cards.health) * 0.1f;
                    st2.text = "HEALTH: " + cards.health;
                    s3.value = float.Parse(cards.defense) * 0.1f;
                    st3.text = "DEFENSE: " + cards.defense;
                    s4.value = 0;
                    st4.text = "";
                    break;
               case 2:
                    s1.value = float.Parse(cards.reloadTime) * 0.1f;
                    st1.text = "RELOAD TIME: " + cards.reloadTime;
                    s2.value = float.Parse(cards.powerBuff) * 0.1f;
                    st2.text = "POWER BUFF: " + cards.powerBuff;
                    s3.value = float.Parse(cards.speedBuff) * 0.1f;
                    st3.text = "SPEED BUFF: " + cards.speedBuff;
                    s4.value = 0;
                    st4.text = "";
                    break;
               case 3:
                    s1.value = float.Parse(cards.power) * 0.1f;
                    st1.text = "POWER: " + cards.power;
                    s2.value = float.Parse(cards.shellSpeed) * 0.1f;
                    st2.text = "SPEED: " + cards.shellSpeed;
                    s3.value = float.Parse(cards.penetration) * 0.1f;
                    st3.text = "PENETRATION: " + cards.penetration;
                    s4.value = float.Parse(cards.deterioration) * 0.1f;
                    st4.text = "DETERIORATION: " + cards.deterioration;
                    break;
               default:
                    Debug.Log("Please give this card the right card tyoe.");
                    break;
          }
     }
}
