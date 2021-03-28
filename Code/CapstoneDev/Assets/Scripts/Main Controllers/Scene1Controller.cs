using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Controller : MonoBehaviour
{
    public Battle[] battles;
    protected static int checkpointAt = 0;

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
        // Start battle with checkpoint if there's a checkpoint reached.
        Debug.Log("Start battle: " + checkpointAt);
        for (int i = checkpointAt; i < battles.Length; i++)
        {
            // Start battle after timer since previous battle started, but only if previous battle has been finished.
            Battle battle = battles[i];
            if (i > checkpointAt)
            {
                yield return new WaitForSeconds(battle.timer);
                Battle prevBattle = battles[i - 1];
                yield return new WaitUntil(() => prevBattle.TestBattleOver());
            }
            // Save a checkpoint if battle is specified to have a checkpoint before it.
            if (battle.checkpointBefore)
                checkpointAt = i;
            battle.StartBattle();
        }
    }

    public static void ResetCheckpoints()
    {
        checkpointAt = 0;
    }
}
