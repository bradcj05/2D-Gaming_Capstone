﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Scene1Controller : MonoBehaviour
{
    public Battle[] battles;

    public AudioSource levelMusic;
    public AudioSource bossMusic;

    public int bossBattleId = 5;
    public float bossWait = 2f;
    protected static int checkpointAt = 0;
    ObjectivesSystem objSys;
    Sidebars hud;

    // For music
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        objSys = GameObject.Find("HUD").GetComponent<ObjectivesSystem>();
        hud = GameObject.Find("HUD").GetComponent<Sidebars>();
        mixer.SetFloat("volume", Mathf.Log(PlayerPrefs.GetFloat("musicVolume", 0.8f)) * 20f);
        StartCoroutine(BattleController());
        ResetCheckpoints();
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
                levelMusic.Play();
            }
            else if (i == bossBattleId)
            {
                bossMusic.Play();
                mixer.SetFloat("bossVolume", 0f);
                StartCoroutine(FadeMixerGroup.Fade(mixer, "bossVolume", 2f, 1f));
            }

            // Save a checkpoint if battle is specified to have a checkpoint before it.
            if (battle.checkpointBefore)
            {
                if (objSys == null)
                    objSys = GameObject.Find("HUD").GetComponent<ObjectivesSystem>();
                checkpointAt = i;
                objSys.CheckpointUpdate();
                Debug.Log("Current Phase: " + checkpointAt);
            }

            // Change phase text & objectives based on phase
            switch (i)
            {
                case 0: // Intro pre-tutorial
                    hud.SetPhaseText("Phase 0/4");
                    objSys.ActivateObjectives(i, -1);
                    break;
                case 1: // Intro post-tutorial
                    break;
                case 2: // Phase 1
                    hud.SetPhaseText("Phase 1/4");
                    objSys.ActivateObjectives(i - 1, -1);
                    break;
                case 3: // Phase 2
                    hud.SetPhaseText("Phase 2/4");
                    objSys.CompleteAutomatic(i - 2, -1);
                    objSys.ActivateObjectives(i - 1, -1);
                    break;
                case 4: // Phase 3
                    hud.SetPhaseText("Phase 3/4");
                    objSys.CompleteAutomatic(i - 2, -1);
                    objSys.ActivateObjectives(i - 1, -1);
                    break;
                case 5: // Phase 4
                    hud.SetPhaseText("Phase 4/4");
                    objSys.ActivateObjectives(3, -1);
                    break;
                case 6: // Boss
                    hud.SetPhaseText("BOSS");
                    objSys.CompleteAutomatic(3, -1);
                    objSys.ActivateObjectives(4, -1);
                    break;
                default:
                    Debug.Log("Error evaluating current phase. Resetting level.");
                    ResetCheckpoints();
                    break;
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
    public int ReturnProgress()
    {
        return checkpointAt;
    }

    public float GetLevelVolumeLinear()
    {
        float value, temp;
        bool result = mixer.GetFloat("levelVolume", out value);
        bool result2 = mixer.GetFloat("volume", out temp);
        if (result)
        {
            Debug.Log("Master Volume: " + temp);
            Debug.Log("Level Volume: " + value);
            return Mathf.Pow(10f, value / 20f);
        }
        else
        {
            return 0f;
        }
    }

    public float GetBossVolumeLinear()
    {
        float value;
        bool result = mixer.GetFloat("bossVolume", out value);
        if (result)
        {
            return Mathf.Pow(10f, value / 20f);
        }
        else
        {
            return 0f;
        }
    }
}
