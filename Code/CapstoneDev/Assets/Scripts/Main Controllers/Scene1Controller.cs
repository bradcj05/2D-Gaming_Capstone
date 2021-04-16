﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Scene1Controller : MonoBehaviour
{
    public Battle[] battles;
    public AudioSource levelMusic;
    public AudioSource bossMusic;
    public int bossBattleId = 4;
    public float bossWait = 2f;
    protected static int checkpointAt = 0;
    ObjectivesSystem objSys;

    // For music
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BattleController());
        objSys = GameObject.Find("HUD").GetComponent<ObjectivesSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Time-based enemy spawner
    IEnumerator BattleController()
    {
        // Start battle with checkpoint if there's a checkpoint reached.
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

            // If it's the boss battle, delay a little before starting for dramatic effect.
            if (i == bossBattleId && i != checkpointAt)
            {
                StartCoroutine(FadeMixerGroup.Fade(mixer, "levelVolume", 2f, 0f));
                yield return new WaitForSeconds(bossWait);
            }
            battle.StartBattle();

            // Play level music or boss music depending on the battle 
            if (i == checkpointAt && i != bossBattleId)
            {
                levelMusic.volume = 0.8f;
                levelMusic.Play();
            }
            else if (i == bossBattleId)
            {
                bossMusic.Play();
                mixer.SetFloat("bossVolume", 0f);
                StartCoroutine(FadeMixerGroup.Fade(mixer, "bossVolume", 2f, 0.8f));
            }

            // Save a checkpoint if battle is specified to have a checkpoint before it.
            if (battle.checkpointBefore)
            {
                if (objSys == null)
                    objSys = GameObject.Find("HUD").GetComponent<ObjectivesSystem>();
                checkpointAt = i;
                Debug.Log("Current Phase: " + checkpointAt);
                //Need to reevaluate how I'm changing objectives
                if (checkpointAt != 0 && checkpointAt != 1)
                    objSys.CompleteAutomatic(checkpointAt - 2, -1);
                if (checkpointAt != 4 && checkpointAt != 5)
                    objSys.ActivateObjectives(checkpointAt - 1, -1);
                else if (checkpointAt == 5)
                    objSys.ActivateObjectives(3, -1);
            }
        }
    }

    public int GetPhase()
    {
        return checkpointAt;
    }

    public static void ResetCheckpoints()
    {
        checkpointAt = 0;
    }
}
