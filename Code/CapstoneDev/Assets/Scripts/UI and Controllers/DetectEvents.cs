using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectEvents : MonoBehaviour
{
    //All work in progress, may not need this script in the future.
    public GameObject player;
    public GameObject levelBoss;

    public GameObject gameOverMenu;
    public int nextSceneLoad;

    protected float levelEndTimer = 0f;
    protected float levelEndTime = 4f;

    public void Awake()
    {
          //Find player squadron
          player = GameObject.Find("Squadron");
          player.GetComponent<PlaneSwitching>().SetUp();
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void Update()
    {
        if (player.GetComponent<PlaneSwitching>().GetIsDead())
        {
            gameOverMenu.SetActive(true);
        }

        // SCENE END BEHAVIOR (when boss is destroyed)
        if (levelBoss == null)
        {
            levelEndTimer += Time.deltaTime;
            if (levelEndTimer >= levelEndTime)
            {
                Debug.Log("boss is dead");
                SceneManager.LoadScene(nextSceneLoad);

                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                    Debug.Log("level unlock");
                }
            }
        }
    }


}
