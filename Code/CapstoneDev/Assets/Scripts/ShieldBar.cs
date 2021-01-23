using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider shieldBar;


    public void SetShield(float shield)
    {
        shieldBar.value = shield;
    }

    public void SetMax(float shield)
    {
        shieldBar.maxValue = shield;
        shieldBar.value = shield;
    }
}
