using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cardDisplay : MonoBehaviour
{
    public GameObject attributes;
    bool purchased = false;

    public Card cards;

    public Image artwork;
    public Text nameText;
    public Text costText;
    public Image lockImage;

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
        // Rotate image to the right
        artwork.sprite = cards.artwork;
        artwork.preserveAspect = false;
        artwork.transform.Rotate(0,0,-90);
        artwork.preserveAspect = true;
        artwork.transform.localScale = new Vector3(2, 2, 2);

        // Set values
        nameText.text = cards.name;
        if (costText != null)
            costText.text = cards.cost.ToString();

        if (cards.unlockLevel == -1 || Progression.progress[cards.unlockLevel])
            lockImage.gameObject.SetActive(false);
        else
            lockImage.gameObject.SetActive(true);

        switch ((int)cards.cardType)
        {
            case 1:
                t1.text = "Speed: " + cards.getSpeed();
                t2.text = "Health: " + cards.getHealth();
                t3.text = "Defense: " + cards.getDefense();
                break;
            case 2:
                t1.text = "RPM: " + cards.getReloadTime();
                t2.text = "Pow Buff: " + cards.getPowerBuff();
                t3.text = "Spd Buff: " + cards.getSpeedBuff();
                break;
            case 3:
                t1.text = "Power: " + cards.getPower();
                t2.text = "Speed: " + cards.getShellSpeed();
                t3.text = "Pen: " + cards.getPenetration();
                break;
            default:
                Debug.Log("Please give this card the right card type.");
                break;
        }
    }

    void Update()
    {
        if (cards.unlockLevel == -1 || Progression.progress[cards.unlockLevel])
            lockImage.gameObject.SetActive(false);
        else
            lockImage.gameObject.SetActive(true);
    }

    // May not be the right script for this function
    public void BuyItem()
    {
        if (cards.unlockLevel != -1 && !Progression.progress[cards.unlockLevel])
        {
            Debug.Log("Item is locked.");
            return;
        }
        if (ScoreTextScript.coinAmount >= cards.cost)
        {
            // Add gameObject to the appropriate list of available objects
            // TODO Fix, something wrong with foreach loop
            foreach (Card card in ObjectList.planeList)
            {
                if (cards == card)
                {
                    Debug.Log("This item was already purchased.");
                    purchased = true;
                    break;
                }
            }

            if (!purchased)
            {
                switch ((int)cards.cardType)
                {
                    case 1:
                        //Maybe add sound effect here to indicate a purchase has been made
                        ObjectList.planeList.Add(cards);
                        HangarMenu.AddButton(cards);
                        break;
                    case 2:
                        ObjectList.gunList.Add(cards);
                        HangarMenu.AddButton(cards);
                        break;
                    case 3:
                        ObjectList.shellList.Add(cards);
                        HangarMenu.AddButton(cards);
                        break;
                    default:
                        Debug.Log("Please give this card the right card type.");
                        break;
                }

                ScoreTextScript.coinAmount -= cards.cost;
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
        switch ((int)cards.cardType)
        {
            case 1:
                s1.value = float.Parse(cards.getSpeed()) * 0.1f;
                st1.text = "SPEED: " + cards.getSpeed();
                s2.value = float.Parse(cards.getHealth()) * 0.1f;
                st2.text = "HEALTH: " + cards.getHealth();
                s3.value = float.Parse(cards.getDefense()) * 0.1f;
                st3.text = "DEFENSE: " + cards.getDefense();
                s4.value = float.Parse(cards.getMass()) * 0.1f;
                st4.text = "MASS: " + cards.getMass();
                break;
            case 2:
                s1.value = 1f / float.Parse(cards.getReloadTime()) * 0.1f;
                st1.text = "FIRE RATE: " + cards.getReloadTime();
                s2.value = float.Parse(cards.getPowerBuff()) * 0.1f;
                st2.text = "POWER BUFF: " + cards.getPowerBuff();
                s3.value = float.Parse(cards.getSpeedBuff()) * 0.1f;
                st3.text = "SPEED BUFF: " + cards.getSpeedBuff();
                s4.value = float.Parse(cards.getSpread()) * 0.1f; ;
                st4.text = "SPREAD: " + cards.getSpread();
                break;
            case 3:
                s1.value = float.Parse(cards.getPower()) * 0.1f;
                st1.text = "POWER: " + cards.getPower();
                s2.value = float.Parse(cards.getShellSpeed()) * 0.1f;
                st2.text = "SPEED: " + cards.getShellSpeed();
                s3.value = float.Parse(cards.getPenetration()) * 0.1f;
                st3.text = "PENETRATION: " + cards.getPenetration();
                s4.value = float.Parse(cards.getDeterioration()) * 0.1f;
                st4.text = "DETERIORATION: " + cards.getDeterioration();
                break;
            default:
                Debug.Log("Please give this card the right card tyoe.");
                break;
        }
    }
}
