using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

     public Slider finalHealth;
    

     public void SetHealth(float health)
     {
        finalHealth.value = health;
     }

     public void SetMax(float health)
     {
        finalHealth.maxValue = health;
        finalHealth.value = health;
     }
}
