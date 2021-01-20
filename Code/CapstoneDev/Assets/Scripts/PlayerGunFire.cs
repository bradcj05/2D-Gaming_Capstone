using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : Gun
{
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer >= waitTime)
        {
            Fire();
            timer = 0f;
        }
    }
}
