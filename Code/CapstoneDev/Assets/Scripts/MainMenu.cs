using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class serves to manage the main menu of the game.
 */
public class MainMenu : MonoBehaviour
{
    //Starts the game
    public void PlayGame()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }

     //Goes to settings menu
     //TODO: Modify this so that we don't have to change it each time the scene order is changed
     public void GoToSettings()
     {
          SceneManager.LoadScene(4);
     }

     //Quits the application
     public void QuitGame()
     {
          Debug.Log("Game Quit");
          Application.Quit();
     }
}
