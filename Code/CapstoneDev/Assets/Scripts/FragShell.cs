using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for Terrod's fragmentation shells
// TODO Finish and TEST
public class FragShell : MonoBehaviour
{
    public int numFragments = 8;
    public bool fixedSpread = true;
    public float spin = 0f;
    public GameObject fragProjectile; // The fragmentation created

    protected float curAngle = 0f;
    protected float angle = 0f;

    // Here to allow for the mathmatics to be implemented
    public void Fracture()
    {
        System.Random rand = new System.Random();

        for (int i = 0; i < numFragments; i++)
        {
            // May need to change the transform.up.x into something else
            if (fixedSpread)
            {
                curAngle = i * (360f / numFragments);
                angle = curAngle + spin;
                // Fire Projectile at angle; Test and FIX
            }
            else
            {
                curAngle = rand.Next(361);
                angle = curAngle + spin;
            }
            // Implements the rotation math from the HM
            GameObject bullet = Instantiate(fragProjectile, transform.position, transform.rotation) as GameObject;
            // Now rotate
            float curRot = transform.localRotation.eulerAngles.z;
            bullet.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot - angle));
            // FRAGMENT WILL BE PROPELLED FORWARD BY ITS OWN SPEED
            // (Trying to manipulate it here is useless)
        }
    }
}
