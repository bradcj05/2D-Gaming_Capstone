using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages the level select menu
public class LevelSelect : MonoBehaviour
{
    public void AirosStart()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }

     public void TerrodStart()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
     }

     public void LynchStart()
     {
          Debug.Log("Coming Soon");
     }
}
