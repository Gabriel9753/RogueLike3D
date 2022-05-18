using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour{
    public static bool isEnabled;

    public GameObject shopUI;
    public int maxUpgrade_dmg;
    public int maxUpgrade_cooldown;
    public int maxUpgrade_frisbee;
    public int maxUpgrade_slow;
    
    
    //Upgrade spell dmg
    public TextMeshProUGUI cost_dmg;
    public TextMeshProUGUI currentUpgrade_dmg;
    public TextMeshProUGUI maxText_dmg;
    public int upgradeLevel_dmg;
    public int upgradeImpact_dmg;
    private int current_cost_dmg;
    
    //Upgrade cooldown
    public TextMeshProUGUI cost_cooldown;
    public TextMeshProUGUI currentUpgrade_cooldown;
    public TextMeshProUGUI maxText_cooldown;
    public int upgradeLevel_cooldown;
    public int upgradeImpact_cooldown;
    private int current_cost_cooldown;
    
    //Upgrade frisbee
    public TextMeshProUGUI cost_frisbee;
    public TextMeshProUGUI currentUpgrade_frisbee;
    public TextMeshProUGUI maxText_frisbee;
    public int upgradeLevel_frisbee;
    public int upgradeImpact_frisbee;
    private int current_cost_frisbee;
    
    //Upgrade slowdown
    public TextMeshProUGUI cost_slowdown;
    public TextMeshProUGUI currentUpgrade_slowdown;
    public int upgradeLevel_slowdown;
    public int upgradeImpact_slowdown;
    private int current_cost_slowdown;
    public TextMeshProUGUI maxText_slow;
    
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

        //current_cost_dmg = cost_start_dmg + cost_inc_dmg * (upgradeLevel_dmg-1);
        float[] functionFactors = calculateCostFunction(55, 75000, 1, maxUpgrade_dmg-1);
        current_cost_dmg = (int)(functionFactors[0] * Math.Pow(Math.E, functionFactors[1] * upgradeLevel_dmg));
        functionFactors = calculateCostFunction(500, 65000, 1, maxUpgrade_cooldown-1);
        current_cost_cooldown = (int)(functionFactors[0] * Math.Pow(Math.E, functionFactors[1] * upgradeLevel_cooldown));
        functionFactors = calculateCostFunction(40, 46350, 1, maxUpgrade_frisbee-1);
        current_cost_frisbee = (int)(functionFactors[0] * Math.Pow(Math.E, functionFactors[1] * upgradeLevel_frisbee));
        functionFactors = calculateCostFunction(645, 64000, 1, maxUpgrade_slow-1);
        current_cost_slowdown = (int)(functionFactors[0] * Math.Pow(Math.E, functionFactors[1] * upgradeLevel_slowdown));

        cost_dmg.text = "" + current_cost_dmg+"g";
        cost_cooldown.text = "" + current_cost_cooldown+"g";
        cost_frisbee.text = "" + current_cost_frisbee+"g";
        cost_slowdown.text = ""+current_cost_slowdown+"g";

        if (upgradeLevel_dmg < maxUpgrade_dmg){
            maxText_dmg.text = "" + maxUpgrade_dmg;
        }
        else{
            maxText_dmg.text = "" + maxUpgrade_dmg;
            upgradeLevel_dmg = maxUpgrade_dmg;
            cost_dmg.text = "max";
        }

        if (upgradeLevel_cooldown < maxUpgrade_cooldown){
            maxText_cooldown.text = "" + maxUpgrade_cooldown;
        }
        else{
            maxText_cooldown.text = "" + maxUpgrade_cooldown;
            upgradeLevel_cooldown = maxUpgrade_cooldown;
            cost_cooldown.text = "max";
        }

        if (upgradeLevel_frisbee < maxUpgrade_frisbee){
            maxText_frisbee.text = "" + maxUpgrade_frisbee;
        }
        else{
            maxText_frisbee.text = "" + maxUpgrade_frisbee;
            upgradeLevel_frisbee = maxUpgrade_frisbee;
            cost_frisbee.text = "max";
        }

        if (upgradeLevel_slowdown < maxUpgrade_slow){
            maxText_slow.text = "" + maxUpgrade_slow;
        }
        else{
            maxText_slow.text = "" + maxUpgrade_slow;
            upgradeLevel_slowdown = maxUpgrade_slow;
            cost_slowdown.text = "max";
        }

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
                if (Player.instance.gold >= current_cost_dmg && upgradeLevel_dmg < maxUpgrade_dmg){
                    AudioManager.instance.Play("gold_upgrade");
                    Player.instance.spell_dmg_up += upgradeImpact_dmg;
                    PlayerPrefs.SetInt("up1", (int)Player.instance.spell_dmg_up);
                    Player.instance.gold -= current_cost_dmg;
                    updateUI();
                }
                break;
            case 2:
                if (Player.instance.gold >= current_cost_cooldown && upgradeLevel_cooldown < maxUpgrade_cooldown){
                    AudioManager.instance.Play("gold_upgrade");
                    Player.instance.cooldown_up += upgradeImpact_cooldown;
                    PlayerPrefs.SetInt("up2", (int)Player.instance.cooldown_up);
                    Player.instance.gold -= current_cost_cooldown;
                    updateUI();
                }
                break;
            case 3:
                if (Player.instance.gold >= current_cost_frisbee && upgradeLevel_frisbee < maxUpgrade_frisbee){
                    AudioManager.instance.Play("gold_upgrade");
                    Player.instance.frisbee_up += upgradeImpact_frisbee;
                    PlayerPrefs.SetInt("up3", (int)Player.instance.frisbee_up);
                    Player.instance.gold -= current_cost_frisbee;
                    updateUI();
                }
                break;
            case 4:
                if (Player.instance.gold >= current_cost_slowdown && upgradeLevel_slowdown < maxUpgrade_slow){
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

    private float[] calculateCostFunction(float startCost, float endCost, float startLevel, float endLevel){
        float[] returnValues = new float[2];
        float u = (float)Math.Pow(Math.E,
            ((Math.Log(startCost) / startLevel) - (Math.Log(endCost) / endLevel)) / (-(1 / endLevel) + (1 / startLevel)));
        float w = (float)(Math.Log(startCost / u) / startLevel);
        returnValues[0] = u;
        returnValues[1] = w;
        return returnValues;
    }
}
