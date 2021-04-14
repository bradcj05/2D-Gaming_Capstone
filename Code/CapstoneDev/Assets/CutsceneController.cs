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
    
    GameObject sceneAiros;
    GameObject sceneCondor;
    Animator airosMove;
    Animator condorMove;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        sceneControl = GameObject.Find("SceneController").GetComponent<Scene1Controller>();
        cutsceneAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Tutorial checks
        isTutorialObjectiveDone = gameObject.GetComponent<Tutorial>().ObjectivesDone();
        if (isTutorialObjectiveDone && !tutorialActivated) StartTutorial();

        //Cutscene checks
        progression = sceneControl.ReturnProgress();
        if(tutorialComplete)
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
        GameObject.FindGameObjectWithTag("ActivePlayer").GetComponent<Animator>().SetBool("freezeplayer", true);
        if (!GameObject.FindGameObjectWithTag("ActivePlayer").GetComponent<Animator>().GetBool("introcomplete"))
        {
            cutsceneAnimator.SetBool("deployDrone", true);
            tutorialActivated = true;
        }
    }

    void StartAirosCutscene()
    {
        sceneAiros = GameObject.Find("Airos");
        airosMove = sceneAiros.GetComponent<Animator>();
        airosMove.SetBool("playIntro", true);
    }

    void StartCondorCutscene()
    {
        sceneCondor = GameObject.Find("Black Condor Phase0");
        condorMove = sceneCondor.GetComponent<Animator>();
        condorMove.SetBool("startFlyBy", true);
        cutsceneAnimator.SetBool("condorAttack", true);
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
        tutorialComplete = true;
    }
    public void CondorIntroDone()
    {
        condorIntroComplete = true;
    }
}
