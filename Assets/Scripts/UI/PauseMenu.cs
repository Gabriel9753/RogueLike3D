using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public static bool gameIsPause = false;

    public GameObject pauseMenuUI;
    public GameObject options;


    public static PauseMenu instance;

    private void Awake(){
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !StatsMenu.instance.statsMenuEnabled && !DieMenu.instance.DieMenuEnabled && !Shop.isEnabled && !OptionsInGame.instance.isEnabled){
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
        OptionsInGame.instance.isEnabled = true;
        pauseMenuUI.SetActive(false);
        options.SetActive(true);
    }

    public void Restart(){
        Resume();
        SceneManager.LoadScene("Spiel");
    }
    public void Exit(){
        Resume();
        SceneManager.LoadScene("StartMenu");
    }
}
