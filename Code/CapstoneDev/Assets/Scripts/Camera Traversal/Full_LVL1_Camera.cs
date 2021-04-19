using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Full_LVL1_Camera : MonoBehaviour
{

    public GameObject cameraOne;
    public GameObject cameraTwo;
    protected Scene1Controller sceneController;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;


    [SerializeField]
    private CinemachineVirtualCamera vcam1, // PHASES 1 & 2
        vcam2,
        vcam3,  // PHASES 3 & 4
        vcam4, vcam5, vcam6, vcam7;

    //cutscene virtual cameras
    [SerializeField]
    private CinemachineVirtualCamera ecam1, ecam2, ecam3, ecam4;




    public float timer = 0;
    public float subTimer = 0;
    // Use this for initialization 

    bool Phase1_2 = true;
    bool PhaseN3, Phase3_4, PhaseNB, PhaseB, Phase5 = false;

    //cutscene virtual camera states
    bool EPhase1, EPhase2, EPhase3, EPhase4, EPhase5 = false;


    // repeat background variables
    public float scrollSpeed;

    //public GameObject[] levels; need 2 for phases 3 and 4
    // assimiate code from Scroll_background

    void Start()
    {
        timer = 0;
        subTimer = 0;
        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();

        //Grab scene controller
        sceneController = GameObject.Find("SceneController").GetComponent<Scene1Controller>();


        //Camera Position Set
        cameraPositionChange(0);

        vcam1.Priority = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // virtual camera priority switching

        // MATT!!!!MATT: "cameraPositionChange(1);" FOR THE CUTSCENES   MATT!!!!MATT

        //!!!
        //HONG  time values for when to switch the cameras
        //!!!

        timer += Time.deltaTime;
        if (PhaseN3)
            subTimer += Time.deltaTime;

        // CHANGE GET PHASE VALUE ON CUTSCENE IMPLEMENTED!
        if (Phase1_2 && sceneController.GetPhase() == 3) /// TRANSITION to GROUND PHASES
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            Phase1_2 = !Phase1_2;
            PhaseN3 = !PhaseN3; //transition
        }
        else if (PhaseN3 && subTimer > 2) //Phase 3 CAMERA ACTIVATE
        {
            vcam2.Priority = 0;
            vcam3.Priority = 1;
            PhaseN3 = !PhaseN3;
            Phase3_4 = !Phase3_4;
        }
        // Load checkpoint from Phase 4
        else if (sceneController.GetPhase() == 4)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 0;
            vcam3.Priority = 0;
            vcam4.Priority = 1;
            Phase1_2 = false;
            PhaseN3 = false;
            Phase3_4 = true;
        }

          if (vcam4.Priority == 1 && Phase3_4 && sceneController.GetPhase() == 4) // dummy test connection between Ground_Phase scripts 
          {
               PhaseNB = !PhaseNB;
               Phase3_4 = !Phase3_4;
               timer = 0;

          }

          //AIROS CAMERAS

          else if (PhaseNB && timer > 2 && sceneController.GetPhase() == 5) //TRANSITION TO AIROS FIGHT
          {
               vcam6.Priority = 0;
               vcam7.Priority = 1;
               PhaseNB = !PhaseNB;
               PhaseB = !PhaseB;


               // cameraPositionChange(1);
          }





     }
    void LateUpdate()
    {

    }



    //Camera change Logic
    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        //Set camera position 1
        if (camPosition == 0)
        {
            cameraOne.SetActive(true);
            cameraOneAudioLis.enabled = true;

            cameraTwoAudioLis.enabled = false;
            cameraTwo.SetActive(false);
        }

        //Set camera position 2
        if (camPosition == 1)
        {
            cameraTwo.SetActive(true);
            cameraTwoAudioLis.enabled = true;

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);


        }

    }


    ///insert scroll_background scripts here.
    ///
}
