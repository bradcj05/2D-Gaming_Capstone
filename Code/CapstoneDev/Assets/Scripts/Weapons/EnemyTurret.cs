using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Turret
{
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        // Initialize player target
        // May have to change player target to something else for allies
        try
        {
            target = GameObject.FindGameObjectWithTag("ActivePlayer").transform;
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
            target = null;
        }
    }

    // Update is called once per frame
    public new void Update()
    {
        //Try to find the next player plane when it spawns
        try
        {
            target = GameObject.FindGameObjectWithTag("ActivePlayer").transform;
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
            target = null;
        }
        base.Update();
    }

    public new void FixedUpdate()
    {
        // Perform turret behavior on player plane
        base.FixedUpdate();
    }
}
