using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;


    public int nextSceneLoad;

    public GameObject levelBoss;
    public GameObject pauseMenuUI;

    void Start() {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }



        if (levelBoss == null)
        {
            Debug.Log("boss is dead");
            SceneManager.LoadScene(nextSceneLoad);

            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt")) {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);

                Debug.Log("level unlock");
            }
        }
    }

    //check if boss is destoryed
  


   public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    //TODO: create a variable for loadscene menu
    public void LoadMenu() {


        Time.timeScale = 1f;
        SceneManager.LoadScene("RickysMainMenu");
        
    }


    public void Quit(){
        Debug.Log("Quiting game");
        Application.Quit();

    }
}