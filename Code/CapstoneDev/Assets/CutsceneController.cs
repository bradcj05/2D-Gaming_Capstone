using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    Animator cutsceneAnimator;
    Scene1Controller sceneControl;
    int progression;
    bool isTutorialObjectiveDone;
    bool tutorialActivated = false;
    bool tutorialComplete = false;
    bool condorIntroComplete = false;
    bool airosIntroActivated = false;
    public float timeforintro = 10;
    public float timefortutorial = 15;
    GameObject sceneAiros;
    GameObject sceneCondor;
    GameObject player;
    Animator airosMove;
    Animator condorMove;
    float timestart, timepassed;
    // Start is called before the first frame update
    void Start()
    {
        sceneControl = GameObject.Find("SceneController").GetComponent<Scene1Controller>();
        cutsceneAnimator = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("ActivePlayer");
        player.GetComponent<Player>().SeizeMovement();
        timestart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timepassed = Time.time;
        isTutorialObjectiveDone = gameObject.GetComponent<Tutorial>().ObjectivesDone();
        if (timepassed - timestart > timeforintro)
        {
            player.GetComponent<Player>().ReleaseMovement();
            if (isTutorialObjectiveDone && !tutorialActivated) StartTutorial();
        }
        if (timepassed - timestart > timefortutorial + timeforintro && tutorialActivated)
        {
            tutorialComplete = true;
        }
        //Tutorial checks
        //player = GameObject.FindWithTag("ActivePlayer");
        
        //if (player.GetComponent<Animator>().GetBool("introcomplete")) EndTutorial();

        //Cutscene checks
        progression = sceneControl.ReturnProgress();
        if(tutorialComplete && !condorIntroComplete)
        {
            StartCondorCutscene();
        }
        if(GameObject.Find("BlackCondor Lv 1") != null)
        {
            StartCondorPhase1();
        }
        if(GameObject.Find("Airos") != null)
        {
            StartAirosCutscene();
        }


    }

    void StartTutorial()
    {
        //player = GameObject.FindWithTag("ActivePlayer");
        //cutsceneAnimator.SetBool("deployDrone", true);
        tutorialActivated = true;
    }

    void EndTutorial()
    {
        //player.GetComponent<Animator>().SetBool("freezeplayer", true);

    }

    void StartAirosCutscene()
    {
        sceneAiros = GameObject.Find("Airos");
        airosMove = sceneAiros.GetComponent<Animator>();
        airosMove.SetBool("playIntro", true);
    }

    void StartCondorCutscene()
    {
        sceneCondor = GameObject.FindWithTag("IntroBlackCondor");
        condorMove = sceneCondor.GetComponent<Animator>();
        condorMove.SetBool("startFlyBy", true);
        cutsceneAnimator.SetBool("condorAttack", true);
        condorIntroComplete = true;
    }

    void StartCondorPhase1()
    {
        sceneCondor = GameObject.Find("BlackCondor Lv 1");
        condorMove = sceneCondor.GetComponent<Animator>();
        cutsceneAnimator.SetBool("eucAttack", true);
        condorMove.SetBool("condorEscape", true);

    }

    public void TutorialTimeDone()
    {
        //tutorialComplete = true;
        cutsceneAnimator.SetBool("deployDrone", false);
    }
    public void CondorIntroDone()
    {
        condorIntroComplete = true;
    }
}
