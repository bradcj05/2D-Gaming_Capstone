using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Full_LVL1_Camera : MonoBehaviour
{

    public GameObject cameraOne;
    public GameObject cameraTwo;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;


    [SerializeField]
    private CinemachineVirtualCamera vcam1;

    [SerializeField]
    private CinemachineVirtualCamera vcam2;

   [SerializeField]
    private CinemachineVirtualCamera vcam3;

    [SerializeField]
    private CinemachineVirtualCamera vcam4;

    [SerializeField]
    private CinemachineVirtualCamera vcam5;

    [SerializeField]
    private CinemachineVirtualCamera vcam6;

    public float timer = 0;
    // Use this for initialization

    bool Phase1 = true;
    bool PhaseN, Phase2, Phase3, Phase4, Phase5 = false;
 


    void Start()
    {

        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();

        //Camera Position Set
        cameraPositionChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Phase1 && timer > 4) ///enable drag
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            Phase1 = !Phase1;
            PhaseN = !PhaseN;

        }
        else if (PhaseN && timer > 8)
        {

            vcam2.Priority = 0;
            vcam3.Priority = 1;
            PhaseN = !PhaseN;
            Phase2 = !Phase2;

        }
        else if (Phase2 && timer > 12)
        {

            vcam3.Priority = 0;
            vcam4.Priority = 1;
            Phase2 = !Phase2;
            Phase3 = !Phase3;
            cameraPositionChange(1);
        }

       // add if statement between theses times ^| to disable drag
        else if (Phase3 && timer > 17)
        {

            vcam4.Priority = 0;
            vcam5.Priority = 1;
            Phase3 = !Phase3;
            Phase4 = !Phase4;
            
        }
        else if (Phase4 && timer > 21)
        {

            vcam5.Priority = 0;
            vcam1.Priority = 1;
            Phase4 = !Phase4;

            cameraPositionChange(0);
        }
    }

 

    //Change Camera Keyboard
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
        //    cameraChangeCounter();
        }
    }


    //Camera change Logic
    void cameraPositionChange(int camPosition)
    {
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
}
