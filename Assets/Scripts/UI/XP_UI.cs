using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build.Content;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class XP_UI : MonoBehaviour
{
    public float currentXP;
    public float maxXP;
    
    public GameObject xpBar;
    public Text levelText;
    public Text xpText;

    public int uncompletedUps;

    #region Singleton
    public static XP_UI Instance;
    //==============================================================
    // Awake
    //==============================================================
    void Awake()
    {
        Instance = this;
        xpBar.GetComponent<Slider>().minValue = 0;
        uncompletedUps = 0;
    }
    #endregion

    public void resetXP(){
        currentXP = 0;
        Player.instance.level = 0;
        uncompletedUps = 0;
        xpBar.GetComponent<Slider>().minValue = 0;
        xpBar.GetComponent<Slider>().value = 0;
        maxXP = calculateMaxXP();
    }
    private float calculateMaxXP(){
        float max_XP = 25 * Player.instance.level + 100;
        return max_XP;
    }

    private bool checkLevelUp(){
        if (currentXP > maxXP){
            return true;
        }
        return false;
    }

    private void levelUp(){
        float overflowXP = currentXP - maxXP;
        Player.instance.level++;
        levelText.GetComponent<Text>().text = "- Level " + Player.instance.level + " -";
        currentXP = overflowXP;
        maxXP = calculateMaxXP();
        xpBar.GetComponent<Slider>().maxValue = calculateMaxXP();
        xpBar.GetComponent<Slider>().value = currentXP;
        LevelUpUpgradesUI.Instance.ActivateUI();
        uncompletedUps++;
    }

    public void setCurrentXP(float amount){
        currentXP = amount;
        updateUI();
    }

    public void updateUI(){
        if (checkLevelUp()){
            levelUp();
        }
        else{
            xpBar.GetComponent<Slider>().maxValue = calculateMaxXP();
            xpBar.GetComponent<Slider>().value = currentXP;
            levelText.GetComponent<Text>().text = "- Level " + Player.instance.level + " -";
        }

        Player.instance.needExp = calculateMaxXP() - currentXP;
    }

    public void addXP(float amount){
        Player.instance.sumExp += amount;
        currentXP += amount;
        float outputXP = amount;
        if (xpText.enabled){
            outputXP = amount + float.Parse(xpText.GetComponent<Text>().text.Trim(new char[]{' ', 'X', 'P', '+'}));
        }
        xpText.GetComponent<Text>().text = "+ " + (int)outputXP + " XP";
        if (!StatsMenu.instance.statsMenuEnabled){
            StartCoroutine(routineXPText(3f));
        }
        updateUI();
    }

    private IEnumerator routineXPText(float duration){
        xpText.enabled = true;
        float timer = 0.0f;
        while(timer < duration){
            timer += Time.deltaTime;
            float t = timer / duration;
            yield return null;
        }
        xpText.enabled = false;
        yield return null;
    }
    
    
        public void SelectedUpgradeAfterLevelUp(int button){
        Upgrade upgrade = UpManager.selected_upgrades[button];
        string uptype = upgrade.GetType().Name;
        switch (uptype){
            case "GoldUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseGold(upgrade.value);
                break; 
            case "GoldMultiplierUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseGold_Multiplier(upgrade.value);
                break;
            case "HealthUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseMaxHealth(upgrade.value);
                break;
            case "HealthRegenUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseHealthRegen(upgrade.value);
                break;
            case "ManaUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseMaxMana(upgrade.value);
                break;
            case "ManaRegenUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseManaRegen(upgrade.value);
                break;
            case "AttackDamageUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseAttackDamage(upgrade.value);
                break;
            case "CriticalChanceUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseCriticalChance(upgrade.value);
                break;
            case "CriticalDamageUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseCriticalDamage(upgrade.value);
                break;
            case "AttackSpeedUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseAttackSpeed(upgrade.value);
                break;
            case "MovementSpeedUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseMovementSpeed(upgrade.value);
                break;
            case "LifestealUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseLifesteal(upgrade.value);
                break;
            case "AwarenessRangeUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseAwarenessRange(upgrade.value);
                break;
            case "ResistanceUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseResistance(upgrade.value);
                break;
            case "EXPMultiplierUpgrade":
                Player.instance.GetComponent<PlayerStats>().IncreaseEXPMultiplier(upgrade.value);
                break;
            
        }
        UpManager.instance.upgradePickByPlayer(uptype);
        updateUI();
    }


}
