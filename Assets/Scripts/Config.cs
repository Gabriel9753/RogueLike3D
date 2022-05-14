using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Config : ScriptableObject{
    #region stats_config
        public int level = 1;
        public float maxHealth = 40; // Maximum amount of health
        public float maxMana = 50;
        public float health = 40; // Current amount of health
        public float mana = 50;
        public float healthRegen = 0.2f;
        public float manaRegen = 0.2f;
        public float gold = 0;
        public float goldMultiplier = 1;
        public float exp = 0;
        public float expMultiplier = 1;
        public float movementSpeed = 10;
        public float resistance = 0;
        public float lifesteal = 0;
        public float attackDamage = 20;
        public float criticalChance = 10;//%
        public float criticalDamage = 10;//%
        public float awarenessRange = 1; //Multiplier
        public float sumExp = 0;
        public float needExp = 0;
        public int killed_mobs = 0;
        public float spell_dmg = 5;//%
        public float spell_1_up = 1;
        public float spell_2_up = 1;
        public float spell_3_up = 1;
        public float spell_4_up = 1;
    #endregion

    #region upgrades_limit
    public int max_gold = 20;
    public int max_mana_up= 10;
    public int max_goldMul_up= 5;
    public int max_manaReg_up= 7;
    public int max_hp_up = 10;
    public int max_hpReg_up = 7;
    public int max_lifesteal_up= 7;
    public int max_ad_up= 10;
    public int max_cc_up = 7;
    public int max_cd_up = 7;
    public int max_awareness_up = 4;
    public int max_expMul_up = 5;
    public int max_movement_up = 4;
    public int max_res_up = 10;
    #endregion

}