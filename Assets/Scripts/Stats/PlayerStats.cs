using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour{
    public int level = 1;
    public float maxHealth = 100; // Maximum amount of health
    public float maxMana = 100;
    public float health = 100; // Current amount of health
    public float mana = 100;
    public float healthRegen = 0.1f;
    public float manaRegen = 0.1f;
    public byte gold = 0;
    public float goldMultiplier;
    public float exp;
    public float expMultiplier;
    public float movementSpeed;
    public float attackSpeed;
    public float resistance;
    public float lifesteal;
    public float baseDamage = 1;
    public float sumDamage = 1;
    public float baseCriticalChance = 1;
    public float sumCriticalChance = 1;
    public float baseCriticalDamage = 1;
    public float sumCriticalDamage = 1;


    public void Start(){
        XP_UI.Instance.updateUI();
        XP_UI.Instance.level = level;
    }

    /** 
    *  GOLD
    */
    public void IncreaseGold(byte goldAmount){
        gold += goldAmount;
    }

    public void DecreaseGold(byte goldAmount){
        gold -= goldAmount;
    }
    /** 
    *  Gold Multiplier
    */
    public void IncreaseGold_Multiplier(float amount){
        goldMultiplier += amount;
    }

    public void DecreaseGold_Multiplier(float amount){
        goldMultiplier -= amount;
    }
    /** 
    *  EXP Multiplier
    */
    public void IncreaseEXP_Multiplier(float amount){
        expMultiplier += amount;
    }

    public void DecreaseEXP_Multiplier(float amount){
        expMultiplier -= amount;
    }
    /** 
    *  ManaRegen
    */
    public void IncreaseManaRegen(float amount){
        manaRegen += amount;
        HealthSystemGUI.instance.SetManaRegen(manaRegen);
    }

    public void DecreaseManaRegen(float amount){
        manaRegen -= amount;
    }
    /** 
    *  MaxMana
    */
    public void IncreaseMaxMana(float amount){
        maxMana += amount;
        HealthSystemGUI.instance.SetMaxMana(maxMana);
    }

    public void DecreaseMaxMana(float amount){
        maxMana -= amount;
    }
    /** 
    *  HealthRegen
    */
    public void IncreaseHealthRegen(float amount){
        healthRegen += amount;
        HealthSystemGUI.instance.SetHealthRegen(healthRegen);
    }
    public void DecreaseHealthRegen(float amount){
        healthRegen -= amount;
    }
    /** 
    *  MaxHealth
    */
    public void IncreaseMaxHealth(float amount){
        maxHealth += amount;
        HealthSystemGUI.instance.SetMaxHealth(maxHealth);
    }

    public void DecreaseMaxHealth(float amount){
        maxHealth -= amount;
    }
    
    

    public void TakeDamage(float damage){
        // Make sure damage doesn't go below 0.
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        // Subtract damage from health
        health -= damage;
        HealthSystemGUI.instance.TakeDamage(damage);
        Debug.Log(transform.name + " takes " + damage + " damage.");

        // If we hit 0. Die.
        if (health <= 0){
            //Die();
        }
    }

    public void Heal(int amount){
        amount = Mathf.Clamp(amount, 0, int.MaxValue);
        health += amount;
        if (health <= 0){
            health = 100;
        }
        HealthSystemGUI.instance.HealDamage(amount);
    }

    public void addPlayerDamage(float dmg){
        sumDamage = baseDamage + dmg;
    }

    public void addPlayerCriticalChance(float critChance){
        sumCriticalChance = baseCriticalChance + critChance;
    }

    public void addPlayerCriticalDamage(float critDmg){
        sumCriticalDamage = baseCriticalDamage + critDmg;
    }
}