using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour{
    public static bool isEnabled;

    public GameObject shopUI;
    
    //Upgrade spell dmg
    public TextMeshProUGUI cost_dmg;
    public TextMeshProUGUI currentUpgrade_dmg;
    public int upgradeLevel_dmg;
    public int upgradeImpact_dmg;
    public int cost_start_dmg;
    public int cost_inc_dmg;
    private int current_cost_dmg;
    
    //Upgrade cooldown
    public TextMeshProUGUI cost_cooldown;
    public TextMeshProUGUI currentUpgrade_cooldown;
    public int upgradeLevel_cooldown;
    public int upgradeImpact_cooldown;
    public int cost_start_cooldown;
    public int cost_inc_cooldown;
    private int current_cost_cooldown;
    
    //Upgrade frisbee
    public TextMeshProUGUI cost_frisbee;
    public TextMeshProUGUI currentUpgrade_frisbee;
    public int upgradeLevel_frisbee;
    public int upgradeImpact_frisbee;
    public int cost_start_frisbee;
    public int cost_inc_frisbee;
    private int current_cost_frisbee;
    
    //Upgrade slowdown
    public TextMeshProUGUI cost_slowdown;
    public TextMeshProUGUI currentUpgrade_slowdown;
    public int upgradeLevel_slowdown;
    public int upgradeImpact_slowdown;
    public int cost_start_slowdown;
    public int cost_inc_slowdown;
    private int current_cost_slowdown;
    // Start is called before the first frame update
    void Start(){
        shopUI.SetActive(false);
        isEnabled = false;
        updateUI();
    }

    private void updateUI(){
        //load upgrade level
        upgradeLevel_dmg = (int) Player.instance.spell_dmg_up / upgradeImpact_dmg;
        upgradeLevel_cooldown = (int) Player.instance.cooldown_up / upgradeImpact_cooldown;
        upgradeLevel_frisbee = (int) Player.instance.frisbee_up / upgradeImpact_frisbee;
        upgradeLevel_slowdown = (int) Player.instance.slowdown_up / upgradeImpact_slowdown;

        currentUpgrade_dmg.text = "" + upgradeLevel_dmg;
        currentUpgrade_cooldown.text = "" + upgradeLevel_cooldown;
        currentUpgrade_frisbee.text = "" + upgradeLevel_frisbee;
        currentUpgrade_slowdown.text = "" + upgradeLevel_slowdown;

        current_cost_dmg = cost_start_dmg + cost_inc_dmg * (upgradeLevel_dmg-1);
        current_cost_cooldown = cost_start_cooldown + cost_inc_cooldown * (upgradeLevel_cooldown-1);
        current_cost_frisbee = cost_start_frisbee + cost_inc_frisbee * (upgradeLevel_frisbee-1);
        current_cost_slowdown = cost_start_slowdown + cost_inc_slowdown * (upgradeLevel_slowdown-1);

        cost_dmg.text = "BUY!\n" + current_cost_dmg;
        cost_cooldown.text = "BUY!\n" + current_cost_cooldown;
        cost_frisbee.text = "BUY!\n" + current_cost_frisbee;
        cost_slowdown.text = "BUY!\n" + current_cost_slowdown;
        
        /*
        PlayerPrefs.SetInt("up1", upgradeLevel_dmg);
        PlayerPrefs.SetInt("up2", upgradeLevel_cooldown);
        PlayerPrefs.SetInt("up3", upgradeLevel_frisbee);
        PlayerPrefs.SetInt("up4", upgradeLevel_slowdown);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.nearUpStation && isEnabled == false){
            if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.gameIsPause){
                isEnabled = true;
                updateUI();
                shopUI.SetActive(true);
            }
        }
    }

    public void upgradeButton(int button){
        switch (button){
            case 1:
                if (Player.instance.gold >= current_cost_dmg && upgradeLevel_dmg < 50){
                    AudioManager.instance.Play("gold_upgrade");
                    Player.instance.spell_dmg_up += upgradeImpact_dmg;
                    PlayerPrefs.SetInt("up1", (int)Player.instance.spell_dmg_up);
                    Player.instance.gold -= current_cost_dmg;
                    updateUI();
                }
                break;
            case 2:
                if (Player.instance.gold >= current_cost_cooldown && upgradeLevel_cooldown < 5){
                    AudioManager.instance.Play("gold_upgrade");
                    Player.instance.cooldown_up += upgradeImpact_cooldown;
                    PlayerPrefs.SetInt("up2", (int)Player.instance.cooldown_up);
                    Player.instance.gold -= current_cost_cooldown;
                    updateUI();
                }
                break;
            case 3:
                if (Player.instance.gold >= current_cost_frisbee && upgradeLevel_frisbee < 20){
                    AudioManager.instance.Play("gold_upgrade");
                    Player.instance.frisbee_up += upgradeImpact_frisbee;
                    PlayerPrefs.SetInt("up3", (int)Player.instance.frisbee_up);
                    Player.instance.gold -= current_cost_frisbee;
                    updateUI();
                }
                break;
            case 4:
                if (Player.instance.gold >= current_cost_slowdown && upgradeLevel_slowdown < 7){
                    AudioManager.instance.Play("gold_upgrade");
                    Player.instance.slowdown_up += upgradeImpact_slowdown;
                    PlayerPrefs.SetInt("up4", (int)Player.instance.slowdown_up);
                    Player.instance.gold -= current_cost_slowdown;
                    updateUI();
                }
                break;
        }
    }

    public void close(){
        isEnabled = false;
        shopUI.SetActive(false);
    }
}
