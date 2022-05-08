
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour{
    private int level;
    private float maxHealth; // Maximum amount of health
    private float maxMana;
    private float health; // Current amount of health
    private float mana;
    private float healthRegen;
    private float manaRegen;
    private float gold;
    private float goldMultiplier;
    private float exp;
    private float expMultiplier;
    private float movementSpeed;
    private float attackSpeed;
    private float resistance;
    private float lifesteal;
    private float attackDamage;
    private float criticalChance;
    private float criticalDamage;
    private float awarenessRange;

    public float sumDamage = 1;
    public float sumCriticalDamage = 1;
    public float sumCriticalChance = 1;


    public void Start(){
        XP_UI.Instance.updateUI();
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

    private void Update(){
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
        Player.instance.gold += goldAmount;
    }

    public void DecreaseGold(float goldAmount){
        gold -= goldAmount;
        Player.instance.gold -= goldAmount;
    }
    /** 
    *  Gold Multiplier
    */
    public void IncreaseGold_Multiplier(float amount){
        goldMultiplier += amount;
        Player.instance.goldMultiplier += amount;
    }

    public void DecreaseGold_Multiplier(float amount){
        goldMultiplier -= amount;
        Player.instance.goldMultiplier-= amount;
    }
    /** 
    *  EXP Multiplier
    */
    public void IncreaseEXPMultiplier(float amount){
        expMultiplier+= amount;
        Player.instance.expMultiplier += amount;
        
    }

    public void DecreaseEXP_Multiplier(float amount){
        expMultiplier -= amount;
        Player.instance.expMultiplier-= amount;
    }
    /** 
    *  ManaRegen
    */
    public void IncreaseManaRegen(float amount){
        manaRegen += amount;
        Player.instance.manaRegen+= amount;
        HealthSystemGUI.instance.SetManaRegen(manaRegen);
    }

    public void DecreaseManaRegen(float amount){
        manaRegen -= amount;
        Player.instance.manaRegen-= amount;
    }
    /** 
    *  MaxMana
    */
    public void IncreaseMaxMana(float amount){
        maxMana += amount;
        Player.instance.maxMana+= amount;
        HealthSystemGUI.instance.SetMaxMana(maxMana);
    }

    public void DecreaseMaxMana(float amount){
        maxMana -= amount;
        Player.instance.maxMana-= amount;
    }
    /** 
    *  HealthRegen
    */
    public void IncreaseHealthRegen(float amount){
        healthRegen += amount;
        Player.instance.healthRegen+= amount;
        HealthSystemGUI.instance.SetHealthRegen(healthRegen);
    }
    public void DecreaseHealthRegen(float amount){
        healthRegen -= amount;
        Player.instance.healthRegen-= amount;
    }
    /** 
    *  MaxHealth
    */
    public void IncreaseMaxHealth(float amount){
        maxHealth += amount;
        Player.instance.maxHealth+= amount;
        HealthSystemGUI.instance.SetMaxHealth(maxHealth);
    }

    public void DecreaseMaxHealth(float amount){
        maxHealth -= amount;
        Player.instance.maxHealth-= amount;
    }
    /** 
    *  AttackSpeed
    */
    public void IncreaseAttackSpeed(float amount){
        attackSpeed += amount;
        Player.instance.attackSpeed+= amount;
        //HealthSystemGUI.instance.SetAttackSpeed(attackSpeed);
    }

    public void AttackSpeed(float amount){
        attackSpeed-= amount;
        Player.instance.attackSpeed-= amount;
    }
    /** 
    *  
    */
    public void IncreaseAwarenessRange(float amount){
        awarenessRange+= amount;
        Player.instance.awarenessRange+= amount;
        
        //HealthSystemGUI.instance.SetAwarenessRange();
    }

    public void DecreaseAwarenessRange(float amount){
        awarenessRange -= amount;
        Player.instance.awarenessRange-= amount;
    }
    /** 
    *  ATTACK DAMAGE
    */
    public void IncreaseAttackDamage(float amount){
        attackDamage+= amount;
        Player.instance.attackDamage+= amount;
        //HealthSystemGUI.instance.SetAttackDamage();
    }

    public void DecreaseBaseAttack(float amount){
        attackDamage-= amount;
        Player.instance.attackDamage-= amount;
    }
    /** 
    *  CRIT CHANCE
    */
    public void IncreaseCriticalChance(float amount){
        criticalChance+= amount;
        Player.instance.criticalChance+= amount;
        //HealthSystemGUI.instance.SetCriticalChance(criticalChance);
    }

    public void DecreaseCriticalChance(float amount){
        criticalChance-= amount;
        Player.instance.criticalChance-= amount;
    }
    /** 
    *  CRIT DAMAGE
    */
    public void IncreaseCriticalDamage(float amount){
        criticalDamage+= amount;
        Player.instance.criticalDamage+= amount;
        //HealthSystemGUI.instance.SetCriticalDamage(criticalDamage);
    }

    public void DecreaseCriticalDamage(float amount){
        criticalDamage-= amount;
        Player.instance.criticalDamage-= amount;
    }
    /** 
    *  LIFESTEAL
    */
    public void IncreaseLifesteal(float amount){
        lifesteal+= amount;
        Player.instance.lifesteal+= amount;
        //HealthSystemGUI.instance.SetLifesteal(lifesteal);
    }

    public void DecreaseLifesteal(float amount){
        lifesteal-= amount;
        Player.instance.lifesteal-= amount;
    }
    /** 
    *  MOVEMENT SPEED
    */
    public void IncreaseMovementSpeed(float amount){
        movementSpeed+= amount;
        Player.instance.movementSpeed+= amount;
        //HealthSystemGUI.instance.SetMovementSpeed(movementSpeed);
    }

    public void DecreaseMovementSpeed(float amount){
        movementSpeed-= amount;
        Player.instance.movementSpeed-= amount;
    }
    /** 
    *  RESISTANCE
    */
    public void IncreaseResistance(float amount){
        resistance+= amount;
        Player.instance.resistance+= amount;
        //HealthSystemGUI.instance.SetResistance(resistance);
    }

    public void DecreaseResistance(float amount){
        resistance-= amount;
        Player.instance.resistance-= amount;
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
    
    
    public float CalculateDamage(float critChance, float critDamage, float rawDamage){
        if (critChance > 100){
            critChance = 100;
        }
        if (Random.Range(0, 100) < critChance){
           Heal((rawDamage + rawDamage * critDamage / 100)*lifesteal);
            return rawDamage + rawDamage * critDamage / 100;
        }
        else{
            Heal(rawDamage*lifesteal);
            return rawDamage;
        }
    }
    
    
    
    public void Heal(float amount){
        amount = Mathf.Clamp(amount, 0, float.MaxValue);
        health += amount;
        if (health > maxHealth){
            health = maxHealth;
        }
        HealthSystemGUI.instance.HealDamage(amount);
    }
    
    
    
    
    
    
    
    //------------------------------ Not considered yet -----------------------------------------



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