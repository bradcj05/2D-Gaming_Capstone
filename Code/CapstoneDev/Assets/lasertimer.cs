using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasertimer : MonoBehaviour
{
    public ParticleSystem laserStartParticles;
    public ParticleSystem prelaserStartParticles;
    public LineRenderer line;
    public LineRenderer preLine;
    private bool startParticlesPlaying = false;
    private bool prestartParticlesPlaying = false;
    private RaycastHit2D hit;
    public float timeBetweenFiring = 10;
    public float preLaserDuration = 2;
    public float laserDuration = 2;
    public float laserLength = 10f;
    public LayerMask layerMask;

    float startTime;
    float timePassed;
    float timeDifference;

    // Start is called before the first frame update
    void Start()
    {
        //line = GetComponent<LineRenderer>();
        startTime = Time.time;
        line.SetPosition(1, new Vector3(laserLength, 0, 0));
        prelaserStartParticles.Stop(true);
        laserStartParticles.Stop(true);
        preLine.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = Time.time;
        timeDifference = timePassed - startTime;
        if ( (timeDifference > timeBetweenFiring) || 
            (timeDifference > preLaserDuration && prestartParticlesPlaying == true) ||
            (timeDifference > laserDuration && startParticlesPlaying == true))
        {
            startTime = Time.time;
            if (prestartParticlesPlaying == false && startParticlesPlaying == false)
            {
                prestartParticlesPlaying = true;
                prelaserStartParticles.Play(true);
                //prelaserStartParticles.gameObject.transform.position = transform.position;
                preLine.enabled = true;
                return;
            }

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
            /**if(startParticlesPlaying == true)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right, laserLength, layerMask);
                if (hit)
                {
                    //addplayer
                    float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
                    line.SetPosition(1, new Vector3(distance, 0, 0));
                }
                else
                {
                    line.SetPosition(1, new Vector3(laserLength, 0, 0));
                }
            }**/

            if (startParticlesPlaying == true && (timeDifference > laserDuration))
            {
                startParticlesPlaying = false;
                laserStartParticles.Stop(true);
                line.enabled = false;
                return;
            }
        }
    }
}
