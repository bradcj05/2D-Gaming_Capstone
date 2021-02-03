using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialFire : SecondaryWeapon
{
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        // Firing code
        if (Input.GetKeyDown(KeyCode.Space) && timer >= waitTime && active)
        {
            Fire();
            timer = 0f;
        }
    }
}
