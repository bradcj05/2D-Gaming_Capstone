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


  //  [SerializeField]
    public CinemachineVirtualCamera vcam1;

   // [SerializeField]
    public CinemachineVirtualCamera vcam2;

  //  [SerializeField]
    public CinemachineVirtualCamera vcam3;

    public float timer = 0;
    // Use this for initialization
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
       
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            

        
         if (timer == 16)
        {

            vcam2.Priority = 0;
            vcam3.Priority = 1;
         

        }
        else if (timer == 21)
        {

            //vcam3.Priority = 0;
            //vcam1.Priority = 1;
            

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

           vcam1= cameraOne.transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>();
           vcam2= cameraOne.transform.GetChild(1).gameObject.GetComponent<CinemachineVirtualCamera>();
           vcam3= cameraOne.transform.GetChild(2).gameObject.GetComponent<CinemachineVirtualCamera>();
           
        }

        //Set camera position 2
        if (camPosition == 1)
        {
            cameraTwo.SetActive(true);
            cameraTwoAudioLis.enabled = true;

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);

            vcam1 = cameraTwo.transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>();
            vcam2 = cameraTwo.transform.GetChild(1).gameObject.GetComponent<CinemachineVirtualCamera>();
            vcam3 = cameraTwo.transform.GetChild(2).gameObject.GetComponent<CinemachineVirtualCamera>();

        }

    }
}
