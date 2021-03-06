using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StatsMenu : MonoBehaviour
{
    #region InspectorInput
    [Header("level")]
    public TextMeshProUGUI level;
    public TextMeshProUGUI level_value;
    [Header("maxHealth")]
    public TextMeshProUGUI maxHealth;
    public TextMeshProUGUI maxHealth_value;
    [Header("Health")]
    public TextMeshProUGUI health;
    public TextMeshProUGUI health_value;
    [Header("maxMana")]
    public TextMeshProUGUI maxMana;
    public TextMeshProUGUI maxMana_value;
    [Header("mana")]
    public TextMeshProUGUI mana;
    public TextMeshProUGUI mana_value;
    [Header("healthRegen")]
    public TextMeshProUGUI healthRegen;
    public TextMeshProUGUI healthRegen_value;
    [Header("manaRegen")]
    public TextMeshProUGUI manaRegen;
    public TextMeshProUGUI manaRegen_value;
    [Header("gold")]
    public TextMeshProUGUI gold;
    public TextMeshProUGUI gold_value;
    [Header("goldMultiplier")]
    public TextMeshProUGUI goldMultiplier;
    public TextMeshProUGUI goldMultiplier_value;
    [Header("exp")]
    public TextMeshProUGUI exp;
    public TextMeshProUGUI exp_value;
    [Header("exp_to_next")]
    public TextMeshProUGUI exp_to_next;
    public TextMeshProUGUI exp_to_next_value;
    [Header("expMultiplier")]
    public TextMeshProUGUI expMultiplier;
    public TextMeshProUGUI expMultiplier_value;
    [Header("movementSpeed")]
    public TextMeshProUGUI movementSpeed;
    public TextMeshProUGUI movementSpeed_value;
    [Header("resistance")]
    public TextMeshProUGUI resistance;
    public TextMeshProUGUI resistance_value;
    [Header("lifesteal")]
    public TextMeshProUGUI lifesteal;
    public TextMeshProUGUI lifesteal_value;
    [Header("attackDamage")]
    public TextMeshProUGUI attackDamage;
    public TextMeshProUGUI attackDamage_value;
    [Header("criticalChance")]
    public TextMeshProUGUI criticalChance;
    public TextMeshProUGUI criticalChance_value;
    [Header("criticalDamage")]
    public TextMeshProUGUI criticalDamage;
    public TextMeshProUGUI criticalDamage_value;
    [Header("awarenessRange")]
    public TextMeshProUGUI awarenessRange;
    public TextMeshProUGUI awarenessRange_value;
    [Header("time")]
    public TextMeshProUGUI time;
    public TextMeshProUGUI time_value;
    [Header("killed_enemies")]
    public TextMeshProUGUI killed_enemies;
    public TextMeshProUGUI killed_enemies_value;
    [Header("spell_dmg")]
    public TextMeshProUGUI spell_dmg;
    public TextMeshProUGUI spell_dmg_value;
    [Header("spell_1")]
    public TextMeshProUGUI spell_1;
    public TextMeshProUGUI spell_1_value;
    [Header("spell_2")]
    public TextMeshProUGUI spell_2;
    public TextMeshProUGUI spell_2_value;
    [Header("spell_3")]
    public TextMeshProUGUI spell_3;
    public TextMeshProUGUI spell_3_value;
    [Header("spell_4")]
    public TextMeshProUGUI spell_4;
    public TextMeshProUGUI spell_4_value;
    

    #endregion

    public bool statsMenuEnabled;
    public GameObject statsUI;
    public KeyCode key;
    public List<GameObject> UI_elements;
    public List<bool> active_elements;

    #region Singleton
    public static StatsMenu instance;
    void Awake ()
    {
        instance = this;
        statsMenuEnabled = false;
        active_elements = new List<bool>();
    }

    #endregion
    void Start(){
        setStats();
    }
    // Start is called before the first frame update
    void setStats(){
        //---------------
        level.text = "Level";
        level_value.text = ""+Player.instance.level;
        //---------------
        maxHealth.text = "max. Health";
        maxHealth_value.text = ""+Math.Round(Player.instance.maxHealth,2);
        //---------------
        maxMana.text = "max. Mana";
        maxMana_value.text = ""+Math.Round(Player.instance.maxMana,2);
        //---------------
        mana.text = "Mana";
        mana_value.text = ""+Math.Round(Player.instance.mana,2);
        //---------------
        mana.text = "Health";
        mana_value.text = ""+Math.Round(Player.instance.health,2);
        //---------------
        healthRegen.text = "Health reg.";
        healthRegen_value.text = ""+Math.Round(Player.instance.healthRegen,2);
        //---------------
        manaRegen.text = "Mana reg.";
        manaRegen_value.text = ""+Math.Round(Player.instance.manaRegen,2);
        //---------------
        gold.text = "Gold";
        gold_value.text = ""+(int)Player.instance.gold;
        //---------------
        goldMultiplier.text = "Gold mult.";
        goldMultiplier_value.text = ""+Math.Round(Player.instance.goldMultiplier,2);
        //---------------
        exp.text = "Sum exp";
        exp_value.text = ""+Math.Round(Player.instance.sumExp,2);
        //---------------
        exp_to_next.text = "Need exp";
        exp_to_next_value.text = ""+Math.Round(Player.instance.needExp,2);
        //---------------
        expMultiplier.text = "Exp mult.";
        expMultiplier_value.text = ""+Math.Round(Player.instance.expMultiplier,2);
        //---------------
        movementSpeed.text = "Move speed";
        movementSpeed_value.text = ""+Math.Round(Player.instance.movementSpeed*10f,2);
        //---------------
        resistance.text = "Defense";
        resistance_value.text = ""+Math.Round(Player.instance.resistance,2);
        //---------------
        lifesteal.text = "Lifesteal";
        lifesteal_value.text = ""+Math.Round(Player.instance.lifesteal,2);
        //---------------
        attackDamage.text = "AD";
        attackDamage_value.text = ""+Math.Round(Player.instance.attackDamage,2);
        //---------------
        criticalChance.text = "CC";
        criticalChance_value.text = ""+Math.Round(Player.instance.criticalChance,2);
        //---------------
        criticalDamage.text = "CD";
        criticalDamage_value.text = ""+Math.Round(Player.instance.criticalDamage,2);
        //---------------
        awarenessRange.text = "Awareness";
        awarenessRange_value.text = ""+Math.Round(Player.instance.awarenessRange,2);
        //---------------
        time.text = "Time";
        float tempTime = WorldManager.instance.timer;
        float seconds = (tempTime / 60 - (int) tempTime);
        if (seconds < 0){
            seconds *= -1;
        }
        time_value.text = ""+(int)tempTime/60+":"+(int)seconds%60;
        //---------------
        killed_enemies.text = "Killed mobs";
        killed_enemies_value.text = ""+Player.instance.killed_mobs;
        //---------------
        spell_dmg.text = "Spell dmg";
        spell_dmg_value.text = ""+Math.Round(Player.instance.spell_dmg_up,2)+"%";
        //---------------
        spell_1.text = "Cooldown";
        spell_1_value.text = "-"+Math.Round(Player.instance.cooldown_up,2)+"%";
        //---------------
        spell_2.text = "Frisbee Up";
        spell_2_value.text = ""+Math.Round(Player.instance.frisbee_up,2);
        //---------------
        spell_3.text = "Slowdown up";
        spell_3_value.text = ""+Math.Round(Player.instance.slowdown_up,2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key) && !DieMenu.instance.DieMenuEnabled){
            if (!statsMenuEnabled){
                foreach (GameObject element in UI_elements){
                    active_elements.Add(element.activeSelf);
                    element.SetActive(false);
                }

                if (!PauseMenu.gameIsPause && !LevelUpUpgradesUI.Instance.uiActive){
                    Time.timeScale = 1f;
                }
                statsMenuEnabled = true;
                statsUI.SetActive(true);
            }
            setStats();
        }
        else{
            if (statsMenuEnabled){
                int counter = 0;
                foreach (GameObject element in UI_elements){
                    element.SetActive(active_elements[counter]);
                    counter++;
                }
                active_elements.Clear();
                if (!PauseMenu.gameIsPause && !LevelUpUpgradesUI.Instance.uiActive){
                    Time.timeScale = 1f;
                }
                else{
                    Time.timeScale = 0f;
                }
                statsUI.SetActive(false);
                statsMenuEnabled = false;
            }
        }
    }
}
