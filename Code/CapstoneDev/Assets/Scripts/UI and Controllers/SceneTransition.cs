using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
     public static int upcomingScene;
     public int scene; //Here to provide testing

     void Update()
     {
          if (scene != upcomingScene)
               upcomingScene = scene;
     }

     //Transitions to the next scene after the Hangar Scene
     public static void NextScene()
     {
          SceneManager.LoadScene(upcomingScene);
     }

     //Transition to the Hangar before the next level
     public void GoToHangar()
     {
          SceneManager.LoadScene("Hangar");
     }
}
