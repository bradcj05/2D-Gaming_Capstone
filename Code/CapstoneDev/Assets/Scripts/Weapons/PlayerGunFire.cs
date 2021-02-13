﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : Gun
{
    protected int activeType;
    protected int numTypes;
    protected bool auto = false;

    // Start
    public new void Start()
    {
        base.Start();
        numTypes = shellTypes.Length;
        activeType = 0;
    }

    // Update is called once per frame
    public new void Update()
    {
        // Switching shell type
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            // scroll up
            activeType = (activeType + 1) % numTypes;
            timer = 0; // Reload upon shell change
        }
        else if (d < 0f)
        {
            // scroll down
            activeType = (activeType + numTypes - 1) % numTypes;
            timer = 0;
        }

        // Engaging auto-fire
        if (Input.GetMouseButtonDown(2))
        {
            auto = !auto;
        }

        // Firing code
        timer += Time.deltaTime;
        if ((Input.GetButton("Fire1") || auto) && timer >= waitTime)
        {
            Fire(shellTypes[activeType]);
            timer = 0f;
        }
    }
}