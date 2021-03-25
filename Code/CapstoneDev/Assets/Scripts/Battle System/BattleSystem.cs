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

public class BattleSystem : MonoBehaviour {

    public event EventHandler OnBattleStarted;
    public event EventHandler OnBattleOver;

    private enum State {
        Idle,
        Active,
        BattleOver,
    }
    
    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Wave[] waveArray;

    private State state;

    private void Awake() {
        state = State.Idle;
    }

    private void Start() {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e) {
        if (state == State.Idle) {
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
    }

    private void StartBattle() {
        Debug.Log("StartBattle");
        state = State.Active;
        OnBattleStarted?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {
        switch (state) {
        case State.Active:
            foreach (Wave wave in waveArray) {
                wave.Update();
            }

            TestBattleOver();
            break;
        }
    }

    private void TestBattleOver() {
        if (state == State.Active) {
            if (AreWavesOver()) {
                // Battle is over!
                state = State.BattleOver;
                Debug.Log("Battle Over!");
                OnBattleOver?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    
    private bool AreWavesOver() {
        foreach (Wave wave in waveArray) {
            if (wave.IsWaveOver()) {
                // Wave is over
            } else {
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
    private class Wave {
        
        [SerializeField] private DestructibleSpawn[] destructibleSpawnArray;
        [SerializeField] private float timer;

        public void Update() {
            if (timer >= 0) {
                timer -= Time.deltaTime;
                if (timer < 0) {
                    SpawnEnemies();
                }
            }
        }

        private void SpawnEnemies() {
            foreach (DestructibleSpawn destructibleSpawn in destructibleSpawnArray) {
                destructibleSpawn.Spawn();
            }
        }

        public bool IsWaveOver() {
            if (timer < 0) {
                // Wave spawned
                foreach (DestructibleSpawn destructibleSpawn in destructibleSpawnArray) {
                    if (destructibleSpawn.IsAlive()) {
                        return false;
                    }
                }
                return true;
            } else {
                // Enemies haven't spawned yet
                return false;
            }
        }
    }

}
