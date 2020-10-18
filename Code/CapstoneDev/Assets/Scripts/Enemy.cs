using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Enemy
{
     void Fire();
     void TakeDamage(float damage);
     void Die();     
}
