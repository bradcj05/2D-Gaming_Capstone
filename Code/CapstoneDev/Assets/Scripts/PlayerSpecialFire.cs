using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialFire : Gun
{
    private bool active = false;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timer >= waitTime)
        {
            Fire();
            timer = 0f;
        }
    }
}
