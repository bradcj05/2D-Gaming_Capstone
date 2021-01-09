using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface for all Enemy Projectiles.
public interface IEnemyProjectile
{
     void OnTriggerEnter2D(Collider2D collision);
}
