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
    private CinemachineVirtualCamera vcam1, vcam2, vcam3, vcam4, vcam5, vcam6, vcam7;

    //cutscene virtual cameras
    [SerializeField]
    private CinemachineVirtualCamera ecam1, ecam2, ecam3, ecam4;




    public float timer = 0;
    // Use this for initialization 

    bool Phase1 = true;
    bool PhaseN, Phase2, PhaseN3, Phase3, PhaseN4, Phase4, Phase5 = false;

    //cutscene virtual camera states
    bool EPhase1, EPhase2, EPhase3, EPhase4, EPhase5 = false;

    
    // repeat background variables
    public GameObject[] level3;
    public GameObject[] level4;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;
    public float scrollSpeed;

    //public GameObject[] levels; need 2 for phases 3 and 4
    // assimiate code from Scroll_background

    void Start()
    {

        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();

        mainCamera = gameObject.GetComponent<Camera>(); /// cameraOne if using the (switch M Camera) and gameObject if using the (MainCamera)
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        //Camera Position Set
        cameraPositionChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        // virtual camera priority switching
        timer += Time.deltaTime;
        if (Phase1 && timer > 4) ///enable drag
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            Phase1 = !Phase1;
            PhaseN = !PhaseN; //transition

        }
        else if (PhaseN && timer > 8) //Phase 2
        {

            vcam2.Priority = 0;
            vcam3.Priority = 1;
            PhaseN = !PhaseN;
            Phase2 = !Phase2;

        }

        else if (Phase2 && timer > 12) //transition
        {

            vcam3.Priority = 0;
            vcam4.Priority = 1;
            Phase2 = !Phase2;
            PhaseN3 = !PhaseN3;
            foreach (GameObject obj in level3)
            {
                loadChildObjects(obj);
            }
            // cameraPositionChange(1);
        }

        // add if statement between theses times ^| to disable drag on player

        else if (PhaseN3 && timer > 15) // PHASE3
        {

            vcam4.Priority = 0;
            vcam5.Priority = 1;
            PhaseN3 = !PhaseN3;
            Phase3 = !Phase3;

        }
        else if (Phase3 && timer > 23)
        { //transition

            vcam5.Priority = 0;
            vcam6.Priority = 1;
            Phase3 = !Phase3;
            PhaseN4 = !PhaseN4;
            foreach (GameObject obj in level4)
            {
                loadChildObjects(obj);
            }
            // cameraPositionChange(0);
        }
        else if (PhaseN4 && timer > 25)
        { // phase 4

            vcam6.Priority = 0;
            vcam7.Priority = 1;
            PhaseN4 = !PhaseN4;
            Phase4 = !Phase4;

        }

        //****trade for translating the self looping background coordinates.

        // scroll the  backgrounds 
       
        //!!!!!!!! This is the main issue
        if (vcam5.Priority == 1 && timer > 16 || vcam7.Priority == 1 && timer > 26)
        {
            Vector3 velocity = Vector3.zero;
            Vector3 desiredPosition = transform.position + new Vector3(0, scrollSpeed, 0); 
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f); ///issues
            transform.position = smoothPosition;                                                                  ///issues
        }


        //if camera priority switch loadchild updates


    }
    void LateUpdate(){
        if (vcam5.Priority == 1 && timer > 16){
            foreach (GameObject obj in level3)
        {
            repositionChildObjects(obj);
            }
        }

        if (vcam7.Priority == 1 && timer > 26)
        {
            foreach (GameObject obj in level4)
            {
                repositionChildObjects(obj);
            }
        }
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
    void loadChildObjects(GameObject obj)
    {
        if (obj == level3[0]|| obj == level4[0])
        {
            choke = 0;
        }
        else
        {
            choke = -3;
        }
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;   //
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);   //
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z); //
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void repositionChildObjects(GameObject obj)
    {
        if (obj == level3[0]|| obj == level4[0]) /// this is the hard way.  we create a new choke value according to the desired beachground object
        {
            choke = 0;
        }
        else
        {
            choke = -3;
        }
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;  //
            if (transform.position.y + screenBounds.y > lastChild.transform.position.y + halfObjectHeight)   //
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjectHeight * 2, lastChild.transform.position.z);  //
            }
            else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfObjectHeight)  //
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.y - halfObjectHeight * 2, firstChild.transform.position.z);  //
            }
            ///(2) a little cleaner if background objects were child objects of the main background, but need to issolate the repeated objects due to a repositioning issue 
        }
    }
}
