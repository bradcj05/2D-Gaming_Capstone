using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages the Game Over menu
//TODO: Implement Game Over into gameplay
public class GameOverMenu : MonoBehaviour
{
    public void Retry()
     {
          //Unsure of how to implement this
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          Debug.Log("I don't know how to make this work.");
     }

     public void BackToMainMenu()
     {
          SceneManager.LoadScene(0);
     }
}
