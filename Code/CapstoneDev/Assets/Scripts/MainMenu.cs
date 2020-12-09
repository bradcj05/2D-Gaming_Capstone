using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

/*
 * This class serves to manage the main menu of the game.
 */
public class MainMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions; 


     public void Start() {

        resolutions  = Screen.resolutions;
        
        //clear out default options to start with a cealn resoulation 
        resolutionDropdown.ClearOptions();

        //converting array to list for add options
        List<string> options = new List<string>();

        //add element option list
        for (int i =0; i < resolutions.Length; i++)
        {
           
            string option = resolutions[i].width +" x "+ resolutions[i].height;

            options.Add(option);

        }

        resolutionDropdown.AddOptions(options);

    }



    public void SetFullScreen (bool isFullscreen) {

        Screen.fullScreen = isFullscreen;
    }   


    //Starts the game
    public void PlayGame()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }

     //Goes to settings menu
     //TODO: Modify this so that we don't have to change it each time the scene order is changed
     public void GoToSettings()
     {
         SceneManager.LoadScene(5);

   
       
    }

    //adjust music 
    public void SetVolume(float volume) {

        audioMixer.SetFloat("volume", volume);
        
    }


     //Quits the application
     public void QuitGame()
     {
          Debug.Log("Game Quit");
          Application.Quit();
     }
}
