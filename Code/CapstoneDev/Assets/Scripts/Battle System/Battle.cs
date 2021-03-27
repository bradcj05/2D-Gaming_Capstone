/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{

    public event EventHandler OnBattleStarted;
    public event EventHandler OnBattleOver;

    private enum State
    {
        Idle,
        Active,
        BattleOver,
    }

    public float timer; // Timer is time the battle will start AFTER THE PREVIOUS BATTLE STARTED
    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Wave[] waveArray;

    private State state;

    protected void Awake()
    {
        state = State.Idle;
    }

    protected void Start()
    {
        if (colliderTrigger != null)
        {
            colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        }
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        if (state == State.Idle)
        {
            StartBattle();
            if (colliderTrigger != null)
            {
                colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
            }
        }
    }

    public void StartBattle()
    {
        Debug.Log("StartBattle");
        state = State.Active;
        OnBattleStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void Update()
    {
        switch (state)
        {
            case State.Active:
                foreach (Wave wave in waveArray)
                {
                    wave.Update();
                }

                TestBattleOver();
                break;
        }
    }

    public void TestBattleOver()
    {
        if (state == State.Active)
        {
            if (AreWavesOver())
            {
                // Battle is over!
                state = State.BattleOver;
                Debug.Log("Battle Over!");
                OnBattleOver?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool AreWavesOver()
    {
        foreach (Wave wave in waveArray)
        {
            if (wave.IsWaveOver())
            {
                // Wave is over
            }
            else
            {
                // Wave not over
                return false;
            }
        }

        return true;
    }


    /*
     * Represents a single Enemy Spawn Wave
     * */
    [System.Serializable]
    private class Wave
    {

        [SerializeField] private DestructibleSpawn[] destructibleSpawnArray;
        [SerializeField] private float timer; // Timer is the time the wave will be started AFTER THE BATTLE STARTS
        [SerializeField] private float waveLength = -1;


        public void Update()
        {
            // Wait for wave to start
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    SpawnEnemies();
                }
            }
            // If wave has already started, wait until it is cancelled, if a cancellation time is specified.
            else if (waveLength > 0)
            {
                waveLength -= Time.deltaTime;
                if (waveLength <= 0)
                {
                    // Cancel wave by calling die on every enemy spawner
                    foreach (DestructibleSpawn destructibleSpawn in destructibleSpawnArray)
                    {
                        destructibleSpawn.Die();
                    }
                }
            }
        }

        private void SpawnEnemies()
        {
            foreach (DestructibleSpawn destructibleSpawn in destructibleSpawnArray)
            {
                if (!destructibleSpawn.gameObject.activeSelf)
                    destructibleSpawn.gameObject.SetActive(true);
                destructibleSpawn.Spawn();
            }
        }

        public bool IsWaveOver()
        {
            if (timer < 0)
            {
                // Wave spawned
                foreach (DestructibleSpawn destructibleSpawn in destructibleSpawnArray)
                {
                    if (destructibleSpawn.IsAlive())
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                // Enemies haven't spawned yet
                return false;
            }
        }
    }

}
