using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// DOES NOT CONTAIN SHOOTING CODE!
// Must be used with another script that containts shooting
// (Like PlayerSpecialFire or PlayerAsynchronousLauncher)

public class SecondaryWeapon : Gun
{
    public int group = 0; // Every secondary weapon in a group will be fired at once
    protected int activeGroup; // The currently active secondary weapon as dictated by player plane
    protected bool active = false;
    protected HealthBar cooldownSlider;
    protected Text ammoText;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        // Setup cooldown slider and ammo text
        cooldownSlider = transform.parent.gameObject.GetComponent<Player>().getCooldownSlider();
        ammoText = transform.parent.gameObject.GetComponent<Player>().getSecondaryAmmo();
    }

    // Update is called once per frame
    public new void Update()
    {
        // Setup cooldown slider and ammo text
        if (cooldownSlider == null)
        {
            cooldownSlider = transform.parent.gameObject.GetComponent<Player>().getCooldownSlider();
        }
        if (ammoText == null)
        {
            ammoText = transform.parent.gameObject.GetComponent<Player>().getSecondaryAmmo();
        }
        // Update timer
        timer += Time.deltaTime;
        // Setup active group
        activeGroup = transform.parent.gameObject.GetComponent<Player>().activeSecondaryWeapon;
        if (group == activeGroup)
        {
            active = true;
        }
        else
        {
            active = false;
        }
        // Set cooldown slider
        if (active && cooldownSlider != null)
        {
            if (ammo > 0)
            {
                cooldownSlider.SetMax(waitTime);
                cooldownSlider.SetHealth(timer);
            }
            else
            {
                cooldownSlider.SetMax(0);
            }
        }
        // Set ammo text
        if (active && ammoText != null)
        {
            ammoText.text = ammo.ToString();
        }
    }
}
