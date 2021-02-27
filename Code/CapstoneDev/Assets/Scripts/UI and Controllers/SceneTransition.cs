using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
     public static int upcomingScene;
     public int scene; //Here to provide testing
     public static GameObject squadron;
     public GameObject squad;

     void Update()
     {
          if (scene != upcomingScene)
               upcomingScene = scene;
          if (squad != squadron)
               squadron = squad;
     }

     //Transitions to the next scene after the Hangar Scene
     public static void NextScene()
     {
          DontDestroyOnLoad(squadron);
          squadron.SetActive(true);
          SceneManager.LoadScene(upcomingScene);
     }

     //Transition to the Hangar before the next level
     public void GoToHangar()
     {
          squadron.SetActive(false);
          SceneManager.LoadScene("Hangar");
     }
}
