using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UpManager : MonoBehaviour{
    [SerializeField] private List<Upgrade> all_upgrades;
    public static List<Upgrade> selected_upgrades;
    private int count_gold_up = 0;
    private int count_goldMul_up = 0;
    private int count_manaReg_up = 0;
    private int count_mana_up = 0;
    private int count_hp_up = 0;
    private int count_hpReg_up = 0;
    private int count_lifesteal_up = 0;
    private int count_ad_up = 0;
    private int count_cc_up = 0;
    private int count_cd_up = 0;
    private int count_awareness_up = 0;
    private int count_expMul_up = 0;
    private int count_movement_up = 0;
    private int count_res_up = 0;

    #region Singleton

    public static UpManager instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion
    public void LevelUpped(){
        selected_upgrades = Select_random_upgrades();
        foreach (var upgrade in selected_upgrades){
            upgrade.SetText(upgrade.Calculate_rnd_value());
        }
    }


    public List<Upgrade> Select_random_upgrades(){
        if (all_upgrades.Count < 3){
            return new List<Upgrade>();
        }
        List<Upgrade> result = new List<Upgrade>();
        int idx;
        for (int i = 0; i < 3; i++){
            idx = Random.Range(0, all_upgrades.Count);
            while (result.Contains(all_upgrades[idx])){
                idx = Random.Range(0, all_upgrades.Count);
            }
            result.Add(all_upgrades[idx]);
        }
        return result;
    }

    public void upgradePickByPlayer(string uptype){
        switch (uptype){
            case "GoldUpgrade":
                count_gold_up++;
                if (count_gold_up == WorldManager.instance.config.max_gold){
                    removeFromList("gold_up");
                }
                break; 
            case "GoldMultiplierUpgrade":
                count_goldMul_up++;
                if (count_goldMul_up == WorldManager.instance.config.max_goldMul_up){
                    removeFromList("goldMul_up");
                }
                break;
            case "HealthUpgrade":
                count_hp_up++;
                if (count_hp_up == WorldManager.instance.config.max_hp_up){
                    removeFromList("hp_up");
                }
                break;
            case "HealthRegenUpgrade":
                count_hpReg_up++;
                if (count_hpReg_up == WorldManager.instance.config.max_hpReg_up){
                    removeFromList("hpReg_up");
                }
                break;
            case "ManaUpgrade":
                count_mana_up++;
                if (count_mana_up == WorldManager.instance.config.max_mana_up){
                    removeFromList("mana_up");
                }
                break;
            case "ManaRegenUpgrade":
                count_manaReg_up++;
                if (count_manaReg_up == WorldManager.instance.config.max_manaReg_up){
                    removeFromList("manaReg_up");
                }
                break;
            case "AttackDamageUpgrade":
                count_ad_up++;
                if (count_ad_up == WorldManager.instance.config.max_ad_up){
                    removeFromList("ad_up");
                }
                break;
            case "CriticalChanceUpgrade":
                count_cc_up++;
                if (count_cc_up == WorldManager.instance.config.max_cc_up){
                    removeFromList("cc_up");
                }
                break;
            case "CriticalDamageUpgrade":
                count_cd_up++;
                if (count_cd_up == WorldManager.instance.config.max_cd_up){
                    removeFromList("cd_up");
                }
                break;
            case "MovementSpeedUpgrade":
                count_movement_up++;
                if (count_movement_up == WorldManager.instance.config.max_movement_up){
                    removeFromList("movement_up");
                }
                break;
            case "LifestealUpgrade":
                count_lifesteal_up++;
                if (count_lifesteal_up == WorldManager.instance.config.max_lifesteal_up){
                    removeFromList("lifesteal_up");
                }
                break;
            case "AwarenessRangeUpgrade":
                count_awareness_up++;
                if (count_awareness_up == WorldManager.instance.config.max_awareness_up){
                    removeFromList("awareness_up");
                }
                break;
            case "ResistanceUpgrade":
                count_res_up++;
                if (count_res_up == WorldManager.instance.config.max_res_up){
                    removeFromList("res_up");
                }
                break;
            case "EXPMultiplierUpgrade":
                count_expMul_up++;
                if (count_expMul_up == WorldManager.instance.config.max_expMul_up){
                    removeFromList("expMul_up");
                }
                break;
            
        }
    }
    private void removeFromList(string type){
        Upgrade up_to_remove = ScriptableObject.CreateInstance<Upgrade>();
        foreach (Upgrade upgrade in all_upgrades){
            if (upgrade.type == type){
                up_to_remove = upgrade;
            }
        }
        all_upgrades.Remove(up_to_remove);
    }
}