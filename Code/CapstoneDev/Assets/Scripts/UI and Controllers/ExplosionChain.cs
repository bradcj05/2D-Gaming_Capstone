using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionChain : MonoBehaviour
{
    public ParticleSystem[] explosion;
    protected bool activateExplosions;
    protected int counter;
    public float explosionTiming = 1f;
    protected float explosionTimer;

    // Start is called before the first frame update
    void Start()
    {
        //explosion = new ParticleSystem[10];
        activateExplosions = false;
        counter = 0;
        explosionTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activateExplosions)
        {
            //explosion[0].Play(true);
            //explosion[counter] != null && 
            explosionTimer += Time.deltaTime;
            if (explosionTimer >= explosionTiming)
            {
                explosion[counter].Play(true);
                explosionTimer = 0f;
                counter++;
            }

            if (counter == explosion.Length)
            {
                activateExplosions = false;
                counter = 0;
            }
        }
    }
    // Trigger explosion chain
    public void TriggerExplosionChain()
    {
        activateExplosions = true;
    }
}
