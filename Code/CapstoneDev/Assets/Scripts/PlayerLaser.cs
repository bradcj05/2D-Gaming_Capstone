using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : SecondaryWeapon
{
    // Effect variables
    public ParticleSystem laserStartParticles;
    public ParticleSystem prelaserStartParticles;
    public LineRenderer line;
    public LineRenderer preLine;
    private bool startParticlesPlaying = false;
    private bool prestartParticlesPlaying = false;

    // Timing variables
    public float preLaserDuration = 2;
    public float laserDuration = 2;
    public float laserLength = 25f;

    // Timer variables
    float startTime;
    float timePassed;
    float timeDifference;

    // Laser damage variables
    public float power; // Maximum power per second
    public float laserEfficiency; // k term in Sigmoid function

    public bool piercing = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        startTime = Time.time;
        line.SetPosition(1, new Vector3(laserLength, 0, 0));
        prelaserStartParticles.Stop(true);
        laserStartParticles.Stop(true);
        preLine.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        // Firing code
        timePassed = Time.time;
        timeDifference = timePassed - startTime;
        if ((timeDifference > waitTime && active && Input.GetKeyDown(KeyCode.Space)) ||
            (timeDifference > preLaserDuration && prestartParticlesPlaying == true) ||
            (timeDifference > laserDuration && startParticlesPlaying == true))
        {
            startTime = Time.time;

            // Charging beam start
            if (prestartParticlesPlaying == false && startParticlesPlaying == false)
            {
                prestartParticlesPlaying = true;
                prelaserStartParticles.Play(true);
                //prelaserStartParticles.gameObject.transform.position = transform.position;
                preLine.enabled = true;
                preLine.SetPosition(1, new Vector3(laserLength, 0, 0));
                return;
            }

            // Laser start
            if (prestartParticlesPlaying == true && (timeDifference > preLaserDuration))
            {
                prestartParticlesPlaying = false;
                startParticlesPlaying = true;
                prelaserStartParticles.Stop(true);
                laserStartParticles.Play(true);
                //laserStartParticles.gameObject.transform.position = transform.position;
                preLine.enabled = false;
                line.enabled = true;
                return;
            }

            // Laser stop
            if (startParticlesPlaying == true && (timeDifference > laserDuration))
            {
                startParticlesPlaying = false;
                laserStartParticles.Stop(true);
                line.enabled = false;
                timer = 0;
                return;
            }
        }
    }

    void FixedUpdate()
    {
        // Hit effect
        if (startParticlesPlaying == true)
        {
            // If piercing laser, ray cast all and deal damage to each target if it's an enemy
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, laserLength);
            foreach (RaycastHit2D hit in hits)
            {
                Enemy e = hit.collider.gameObject.GetComponent<Enemy>();
                if (e != null)
                {
                    // Take damage by Sigmoid function
                    e.TakeDamage((power - e.defense) * Time.deltaTime * (2 / (1 + Mathf.Exp(-timeDifference * laserEfficiency)) - 1));
                    // If not piercing, reduce laser distance and break out of loop (do not damage other enemies)
                    // Shorten laser to hit point only
                    if (!piercing)
                    {
                        float distance = (hit.point - (Vector2)transform.position).magnitude;
                        line.SetPosition(1, new Vector3(distance, 0, 0));
                        break;
                    }
                }
            }
        }
    }
}
