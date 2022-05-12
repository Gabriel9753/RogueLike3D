using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject optionsUI;
    public GameObject startMenuUI;
    
    public TextMeshProUGUI level;
    public TextMeshProUGUI level_v;
    public TextMeshProUGUI time;
    public TextMeshProUGUI time_v;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI gold_v;
    public TextMeshProUGUI killedEnemies;
    public TextMeshProUGUI killedEnemies_v;
    // Start is called before the first frame update
    void Start(){
        //Save Level, Gold, Time, Killes enemies
        if (PlayerPrefs.HasKey("level") && PlayerPrefs.HasKey("time") && PlayerPrefs.HasKey("gold") &&
            PlayerPrefs.HasKey("killedEnemies")){
            level.text = "Level";
            level_v.text = ""+PlayerPrefs.GetInt("level");
            time.text = "Time";
            time_v.text = ""+(int)PlayerPrefs.GetFloat("time");
            gold.text = "Gold";
            gold_v.text = ""+PlayerPrefs.GetInt("gold");
            killedEnemies.text = "Killed Enemies";
            killedEnemies_v.text = ""+PlayerPrefs.GetInt("killedEnemies");
        }
        else{
            level.text = "Level";
            level_v.text = "Not yet!";
            time.text = "Time";
            time_v.text = "Not yet!";
            gold.text = "Gold";
            gold_v.text = "Not yet!";
            killedEnemies.text = "Killed Enemies";
            killedEnemies_v.text = "Not yet!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene("Spiel");
    }
    public void StartOptions(){
        print("Options!");
        optionsUI.SetActive(true);
        startMenuUI.SetActive(false);
    }

    public void ExitGame(){
        print("QUIT!");
        Application.Quit();
    }
    
    public void ResetButton(){
        PlayerPrefs.DeleteAll();
        Start();
    }
}
