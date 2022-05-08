using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour{
    public int level;
    public float maxHealth; // Maximum amount of health
    public float maxMana;
    public float health; // Current amount of health
    public float mana;
    public float healthRegen;
    public float manaRegen;
    public float gold;
    public float goldMultiplier;
    public float exp;
    public float expMultiplier;
    public float movementSpeed;
    public float attackSpeed;
    public float resistance;
    public float lifesteal;
    public float attackDamage;
    public float criticalChance;
    public float criticalDamage;
    public float awarenessRange;

    public float sumDamage = 1;
    public float sumCriticalDamage = 1;
    public float sumCriticalChance = 1;


    public void Start(){
        XP_UI.Instance.updateUI();
        XP_UI.Instance.level = level;
        print(Player.instance.expMultiplier);
        level = Player.instance.level;
        maxHealth = Player.instance.maxHealth; // Maximum amount of health
        maxMana = Player.instance.maxMana;
        health = Player.instance.health; // Current amount of health
        mana = Player.instance.mana;
        healthRegen = Player.instance.healthRegen;
        manaRegen = Player.instance.manaRegen;
        gold = Player.instance.gold;
        goldMultiplier = Player.instance.goldMultiplier;
        exp =Player.instance.exp;
        expMultiplier = Player.instance.expMultiplier;
        movementSpeed = Player.instance.movementSpeed;
        attackSpeed = Player.instance.attackSpeed;
        resistance = Player.instance.resistance;
        lifesteal =Player.instance.lifesteal;
        attackDamage = Player.instance.attackDamage;
        criticalChance = Player.instance.criticalChance;
        criticalDamage = Player.instance.criticalDamage;
        awarenessRange = Player.instance.awarenessRange;
    }

    #region Upgrade Increasing / Decreasing Methods
    /** 
    *  GOLD
    */
    public void IncreaseGold(float goldAmount){
        gold += goldAmount;
    }

    public void DecreaseGold(float goldAmount){
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
    public void IncreaseEXPMultiplier(float amount){
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
    /** 
    *  AttackSpeed
    */
    public void IncreaseAttackSpeed(float amount){
        attackSpeed += amount;
        //HealthSystemGUI.instance.SetAttackSpeed(attackSpeed);
    }

    public void AttackSpeed(float amount){
        attackSpeed-= amount;
    }
    /** 
    *  
    */
    public void IncreaseAwarenessRange(float amount){
        awarenessRange+= amount;
        
        //HealthSystemGUI.instance.SetAwarenessRange();
    }

    public void DecreaseAwarenessRange(float amount){
        awarenessRange -= amount;
    }
    /** 
    *  ATTACK DAMAGE
    */
    public void IncreaseAttackDamage(float amount){
        attackDamage+= amount;
        //HealthSystemGUI.instance.SetAttackDamage();
    }

    public void DecreaseBaseAttack(float amount){
        attackDamage-= amount;
    }
    /** 
    *  CRIT CHANCE
    */
    public void IncreaseCriticalChance(float amount){
        criticalChance+= amount;
        //HealthSystemGUI.instance.SetCriticalChance(criticalChance);
    }

    public void DecreaseCriticalChance(float amount){
        criticalChance-= amount;
    }
    /** 
    *  CRIT DAMAGE
    */
    public void IncreaseCriticalDamage(float amount){
        criticalDamage+= amount;
        //HealthSystemGUI.instance.SetCriticalDamage(criticalDamage);
    }

    public void DecreaseCriticalDamage(float amount){
        criticalDamage-= amount;
    }
    /** 
    *  LIFESTEAL
    */
    public void IncreaseLifesteal(float amount){
        lifesteal+= amount;
        //HealthSystemGUI.instance.SetLifesteal(lifesteal);
    }

    public void DecreaseLifesteal(float amount){
        lifesteal-= amount;
    }
    /** 
    *  MOVEMENT SPEED
    */
    public void IncreaseMovementSpeed(float amount){
        movementSpeed+= amount;
        //HealthSystemGUI.instance.SetMovementSpeed(movementSpeed);
    }

    public void DecreaseMovementSpeed(float amount){
        movementSpeed-= amount;
    }
    /** 
    *  RESISTANCE
    */
    public void IncreaseResistance(float amount){
        resistance+= amount;
        //HealthSystemGUI.instance.SetResistance(resistance);
    }

    public void DecreaseResistance(float amount){
        resistance-= amount;
    }
    
    #endregion
    

    public void TakeDamage(float damage){
        // Make sure damage doesn't go below 0.
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        // damage calculation with respect of resistance
        damage -= resistance;
        health -= damage;
        HealthSystemGUI.instance.TakeDamage(damage);

        // If we hit 0. Die.
        if (health <= 0){
            //Die();
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    //------------------------------ Not considered yet -----------------------------------------

    public void Heal(int amount){
        amount = Mathf.Clamp(amount, 0, int.MaxValue);
        health += amount;
        if (health <= 0){
            health = 100;
        }
        HealthSystemGUI.instance.HealDamage(amount);
    }

    public void addPlayerDamage(float dmg){
        sumDamage = attackDamage + dmg;
    }

    public void addPlayerCriticalChance(float critChance){
        sumCriticalChance = criticalChance + critChance;
    }

    public void addPlayerCriticalDamage(float critDmg){
        sumCriticalDamage = criticalDamage + critDmg;
    }
}