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
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          Debug.Log("Reloading Scene");
     }

     public void BackToMainMenu()
     {
          SceneManager.LoadScene(0);
          Debug.Log("Back to Main Menu");
     }
}
