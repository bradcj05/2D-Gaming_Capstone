using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Controller : BattleSystem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Time-based enemy spawner
    IEnumerator EnemySpawner()
    {
        // -- PHASE 1 --
        yield return new WaitForSeconds(5);
    }
}
