using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieMenu : MonoBehaviour{
    public GameObject DieMenuUI;
    public bool newHighscore = false;
    public List<GameObject> UIElements;
    public bool DieMenuEnabled = false;

    int tempLevel;
    float tempTime;
    int tempGold;
    int tempkilledEnemies;
    
    public TextMeshProUGUI level;
    public TextMeshProUGUI level_v;
    public TextMeshProUGUI time;
    public TextMeshProUGUI time_v;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI gold_v;
    public TextMeshProUGUI killedEnemies;
    public TextMeshProUGUI killedEnemies_v;

    #region Singleton

    public static DieMenu instance;

    void Awake ()
    {
        instance = this;
        DieMenuUI.SetActive(false);
        newHighscore = false;
    }

    #endregion
    
    public void dieMenuStart(){
        foreach (GameObject element in UIElements){
            element.SetActive(false);
        }
        DieMenuUI.SetActive(true);
        DieMenuEnabled = true;
        tempLevel = PlayerPrefs.GetInt("level");
        tempTime = PlayerPrefs.GetFloat("time");
        tempGold = PlayerPrefs.GetInt("gold");
        tempkilledEnemies = PlayerPrefs.GetInt("killedEnemies");
        saveStats();
        showStats();
    }

    private void showStats(){
        level.text = "Level";
        level_v.text = ""+Player.instance.level;
        time.text = "Time";
        time_v.text = ""+(int)WorldManager.instance.timer + "s";
        gold.text = "Gold";
        gold_v.text = ""+(int)Player.instance.gold;
        killedEnemies.text = "Killed Enemies";
        killedEnemies_v.text = ""+Player.instance.killed_mobs;
    }

    public void saveStats(){
        //Save Level, Gold, Time, Killes enemies
        if (PlayerPrefs.HasKey("level") && PlayerPrefs.HasKey("time") && PlayerPrefs.HasKey("gold") && PlayerPrefs.HasKey("killedEnemies")){
            
            if (tempLevel == Player.instance.level){
                if (tempTime < WorldManager.instance.timer){
                    //Same level, better time:
                    save();
                }
            }
            if (tempLevel < Player.instance.level){
                save();
            }
        }
        else{
            save();
        }
    }

    private void save(){
        PlayerPrefs.SetInt("level", Player.instance.level);
        PlayerPrefs.SetFloat("time", WorldManager.instance.timer);
        PlayerPrefs.SetInt("gold", (int)Player.instance.gold);
        PlayerPrefs.SetInt("killedEnemies", Player.instance.killed_mobs);
        newHighscore = true;
    }

    public void restart(){
        PlayerPrefs.SetInt("restartGold", (int)Player.instance.gold);
        SceneManager.LoadScene("Spiel");
        
        
        /*DieMenuUI.SetActive(false);
        DieMenuEnabled = false;
        XP_UI.Instance.resetXP();
        SetupWorld.instance.setup(true);*/
        
    }

    public void exit(){
        DieMenuUI.SetActive(false);
        DieMenuEnabled = false;
        PlayerPrefs.SetInt("restartGold", (int)Player.instance.gold);
        SceneManager.LoadScene("StartMenu");
    }

}
