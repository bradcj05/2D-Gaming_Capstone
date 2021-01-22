using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrodMainTurret : EnemyTurret
{
     public int attack = 0;
     float beat;
     GameObject activeType;
     int numShellsFiredInAttack;
     public int numShellsPerAttack = 4;
     int activeBulletSpawn = 0;
     System.Random rand;

     // Start is called before the first frame update
     public new void Start()
     {
          base.Start();

          // Determine which attack pattern the Main Turret starts with
          rand = new System.Random();
          double sentinel = rand.NextDouble(); // NextDouble produces a random double >= 0 and < 1
          // Prop (attack 1) = 0.25
          // Prop (attack 2) = 0.25
          // Prop (attack 3) = 0.5
          if (sentinel < 0.25)
               attack = 1;
          else if (sentinel < 0.5)
               attack = 2;
          else
               attack = 3;

          numShellsFiredInAttack = 0;
          beat = waitTime / bulletSpawns.Length;
     }

     // Update is called once per frame
     public new void Update()
     {
          // Update timer
          timer += Time.deltaTime;
          if (timer >= waitTime)
          {
               Fire();
               timer -= waitTime;
          }
     }

     // New Fire function to accomidate for additional attack patterns
     new void Fire()
     {
          // Declare values
          float curRot;
          Quaternion bulletAngle;
          GameObject bullet;
          Rigidbody2D rig;
          float bulletSpeed;

          switch (attack)
          {
               case 1:
                    // Using shellType[0]
                    activeType = shellType[0];
                    base.Fire();
                    numShellsFiredInAttack += bulletSpawns.Length;
                    if (numShellsFiredInAttack >= numShellsPerAttack)
                         SwitchAttack(attack);
                    break;
               case 2:
                    // Using shellType[1]
                    activeType = shellType[1]; // Make sure shellType[1] is not null

                    // Adding the code for the base Fire function
                    foreach (Transform bulletSpawn in bulletSpawns)
                    {
                         // Account for spread by generating random angle
                         curRot = bulletSpawn.rotation.eulerAngles.z;
                         bulletAngle = Quaternion.Euler(new Vector3(0, 0, curRot + Random.Range(-spread / 2, spread / 2)));
                         // Create bullet
                         bullet = Instantiate(activeType, bulletSpawn.position, bulletAngle) as GameObject;
                         rig = bullet.GetComponent<Rigidbody2D>();
                         // Apply speed and power buff
                         bulletSpeed = bullet.GetComponent<Bullet>().speed * (1 + speedBuff);
                         bullet.GetComponent<Bullet>().power *= (1 + powerBuff);
                         // Push bullet
                         rig.velocity = bulletSpawn.up * bulletSpeed;
                    }

                    numShellsFiredInAttack += bulletSpawns.Length;
                    if (numShellsFiredInAttack >= numShellsPerAttack)
                         SwitchAttack(attack);
                    break;
               case 3:
                    // Shoot each gun alternatively, shell type random
                    // TODO fix 
                    activeType = shellType[rand.Next(shellType.Length)];
                    activeBulletSpawn = (activeBulletSpawn + 1) % bulletSpawns.Length;

                    // Account for spread by generating random angle
                    curRot = bulletSpawns[activeBulletSpawn].rotation.eulerAngles.z;
                    bulletAngle = Quaternion.Euler(new Vector3(0, 0, curRot + Random.Range(-spread / 2, spread / 2)));
                    // Create bullet
                    bullet = Instantiate(activeType, bulletSpawns[activeBulletSpawn].position, bulletAngle) as GameObject;
                    rig = bullet.GetComponent<Rigidbody2D>();
                    // Apply speed and power buff
                    bulletSpeed = bullet.GetComponent<Bullet>().speed * (1 + speedBuff);
                    bullet.GetComponent<Bullet>().power *= (1 + powerBuff);
                    // Push bullet
                    rig.velocity = bulletSpawns[activeBulletSpawn].up * bulletSpeed;
                    numShellsFiredInAttack++;

                    if (numShellsFiredInAttack >= numShellsPerAttack)
                    {
                         SwitchAttack(attack);
                    } else
                    {
                         // Wait for beat amount of seconds
                         // May need to find a better way of doing things
                         float attack3Timer = 0f;
                         while (attack3Timer < beat)
                         {
                              attack3Timer += Time.deltaTime;
                         }
                         Fire();
                    }
                    break;
               default:
                    // Added a Debug message in case attack is not 1, 2, or 3.
                    Debug.Log("The attack value for TerrodMainTurret should not be this value.");
                    break;
          }
     }

     // Switches which of the attacks the Main Cannon will do
     void SwitchAttack(int a)
     {
          // First, reset the counter for the number of bullets spawned for an attack
          numShellsFiredInAttack = 0;
          double sentinel;
          switch (a)
          {
               case 1:
                    sentinel = rand.NextDouble(); // NextDouble produces a random double >= 0 and < 1
                    if (sentinel < 0.5)
                         attack = 2;
                    else
                         attack = 3;
                    break;
               case 2:
                    sentinel = rand.NextDouble(); // NextDouble produces a random double >= 0 and < 1
                    if (sentinel < 0.5)
                         attack = 1;
                    else
                         attack = 3;
                    break;
               case 3:
                    sentinel = rand.NextDouble(); // NextDouble produces a random double >= 0 and < 1
                    if (sentinel < 0.5)
                         attack = 1;
                    else
                         attack = 2;
                    break;
               default:
                    // Added a Debug message in case attack is not 1, 2, or 3.
                    Debug.Log("The attack value for TerrodMainTurret should not be this value.");
                    break;
          }
     }
}
