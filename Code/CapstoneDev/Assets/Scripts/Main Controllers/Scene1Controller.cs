using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Controller : MonoBehaviour
{
    public Battle[] battles;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Time-based enemy spawner
    IEnumerator EnemySpawner()
    {
        // -- PHASE 1 --

        // -- PHASE 2 --

        // -- PHASE 3 --

        // -- PHASE 4 --

        // -- BOSS (Test) --
        foreach (Battle battle in battles)
        {
            yield return new WaitForSeconds(battle.timer);
            battle.StartBattle();
        }
    }
}
