using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseBar : MonoBehaviour
{
    public Slider defenseHealth;


    public void SetDefense(float defense)
    {
        defenseHealth.value = defense;
    }

    public void SetMax(float defense)
    {
        defenseHealth.maxValue = defense;
        defenseHealth.value = defense;
    }
}
