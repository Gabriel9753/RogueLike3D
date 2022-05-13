using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public static bool gameIsPause = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !StatsMenu.instance.statsMenuEnabled && !DieMenu.instance.DieMenuEnabled && !Shop.isEnabled){
            if (gameIsPause){
                Resume();
            }
            else{
                Pause();
            }
        }    
    }

    private void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
        Player.instance._agent.ResetPath();
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    public void Options(){
    }
    public void Exit(){
        Resume();
        SceneManager.LoadScene("Spiel");
    }
}
