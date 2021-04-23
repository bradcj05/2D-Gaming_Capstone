using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    Animator cutsceneAnimator;
    Scene1Controller sceneControl;
    int progression;
    public bool introNeeded = true;
    bool isTutorialObjectiveDone;
    bool tutorialActivated = false;
    bool tutorialComplete = false;
    bool condorIntroComplete = false;
    bool airosIntroActivated = false;
    bool condorFled = false;
    bool airosArrived = false;
    public float timeforintro = 10;
    public float timefortutorial = 15;
    public GameObject Scene1Airos;
    GameObject sceneCondor;
    public GameObject Scene1BlackCondor;
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
        if(introNeeded) player.GetComponent<Player>().SeizeMovement();
        timestart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timepassed = Time.time;
        isTutorialObjectiveDone = gameObject.GetComponent<Tutorial>().ObjectivesDone();
        if (timepassed - timestart > timeforintro && introNeeded)
        {
            player.GetComponent<Player>().ReleaseMovement();
            if (isTutorialObjectiveDone && !tutorialActivated) StartTutorial();
        }
        if (timepassed - timestart > timefortutorial + timeforintro && tutorialActivated && introNeeded)
        {
            tutorialComplete = true;
        }

        //Tutorial checks
        //player = GameObject.FindWithTag("ActivePlayer");
        
        //if (player.GetComponent<Animator>().GetBool("introcomplete")) EndTutorial();

        //Cutscene checks
        progression = sceneControl.ReturnProgress();
        if(GameObject.FindWithTag("IntroCondor") != null && !condorIntroComplete)
        {
            StartCondorCutscene();
        }
        if(GameObject.FindWithTag("Lv1BlackCondor") != null && !condorFled)
        {
            StartCondorFlees();
        }
        if(GameObject.FindWithTag("Airos") != null && !airosArrived)
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
        //Scene1Airos = GameObject.Find("Airos");
        airosMove = Scene1Airos.GetComponent<Animator>();
        airosMove.SetBool("playIntro", true);
        airosArrived = true;
    }

    void StartCondorCutscene()
    {
        sceneCondor = GameObject.FindWithTag("IntroBlackCondor");
        condorMove = sceneCondor.GetComponent<Animator>();
        condorMove.SetBool("startFlyBy", true);
        cutsceneAnimator.SetBool("condorAttack", true);
        condorIntroComplete = true;
    }

    void StartCondorFlees()
    {
        //Scene1BlackCondor = GameObject.Find("Black Condor Lv 1");
        sceneCondor.SetActive(false);
        condorMove = Scene1BlackCondor.GetComponent<Animator>();
        cutsceneAnimator.SetBool("eucAttack", true);
        condorMove.SetBool("condorEscape", true);
        player.GetComponent<Animator>().SetBool("dodgebomb", true);
        condorFled = true;
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
