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
            target = GameObject.FindGameObjectWithTag("Player").transform;
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
        base.Update();
    }
}
