using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour{
    [HideInInspector]public Animator animator;
    [HideInInspector]public NavMeshAgent _agent;
    public new Camera camera;
    public GameObject weapon;
    public GameObject leftHand;
    [HideInInspector]public Vector3 destination;

    [HideInInspector]public bool isDead;
    [HideInInspector]public static bool nearDoor;
    [HideInInspector]public static bool nearUpStation;
    [HideInInspector]public static bool nearTeleporter;

    [HideInInspector]public bool isRolling;
    
    [Header("Player stats")]
    #region Player stats

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
    public float criticalChance;//%
    public float criticalDamage;//%
    public float awarenessRange; //Multiplier

    public float sumExp = 0;
    public float needExp = 0;
    public int killed_mobs = 0;
    
    public float spell_dmg_up;
    public float cooldown_up;
    public float frisbee_up;
    public float slowdown_up;

    #endregion
    
    [Space]
    [Header("Others")]
    public Material matNormalWeapon;
    public Material matBuffedWeapon;
    #region Singleton

    public static Player instance;

    void Awake (){
        isRolling = false;
        instance = this;
        animator = instance.GetComponent<Animator>();
        _agent = instance.GetComponent<NavMeshAgent>();
        destination = _agent.destination;
        instance.GetComponent<AbilityHolder>().enabled = false;
        isDead = false;
        resetStats();
        load_upgrades();
    }

    #endregion

    public void Die(){
        SpawnManager.instance.killAllEnemies();
        isDead = true;
       //TODO:show screen and stats
       instance._agent.enabled = false;
       instance.GetComponent<CapsuleCollider>().enabled = false;
       instance.GetComponent<PlayerMovement>().enabled = false;
       instance.GetComponent<PlayerAttack>().enabled = false;
       instance.GetComponent<PlayerCombo>().enabled = false;
       instance.GetComponent<PlayerStats>().enabled = false;
       instance.GetComponent<AbilityHolder>().enabled = false;
       WorldManager.instance.stopTime = false;
       
       DieMenu.instance.dieMenuStart();
    }

    #region Check animation states from player

    public bool isAttacking(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_1"))
            return true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_2"))
            return true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_3"))
            return true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("MagicOrb"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Laserbeam"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Heal"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Cosmic"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightningFrisbee"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("fireHurricane"))
            return true;

        return false;
    }

    public bool isCasting(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("MagicOrb"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Laserbeam"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Heal"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Cosmic"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightningFrisbee"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("fireHurricane"))
            return true;
        return false;
    }

    public bool standAttack(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_1"))
            return true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_2"))
            return true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_3"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("MagicOrb"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Laserbeam"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Heal"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Cosmic"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightningFrisbee"))
            return true;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("fireHurricane"))
            return true;
        return false;
    }
    public bool moveAttack(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
            return true;
        return false;
    }
    
    
    public bool isRunning(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            return true;
        return false;
    }
    
    public bool isDashing(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            return true;
        return false;
    }

    public bool isHit(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerHit"))
            return true;
        return false;
    }

    public bool isChannelingAbility(){
        //TODO
        return false;
    }

    public String GetSpeedMultiplierName(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_1"))
            return "Normal1";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_2"))
            return "Normal2";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_3"))
            return "Normal3";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
            return "run Attack";
        
        //ADD NEW ATTACK OR ABILITY HERE
        
        return "";
    }
    
    public String GetAnimationName(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_1"))
            return "Normal_Attack_1";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_2"))
            return "Normal_Attack_2";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_3"))
            return "Normal_Attack_3";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
            return "RunAttack";
        
        //ADD NEW ATTACK OR ABILITY HERE
        
        return "";
    }

    #endregion
    public void setCamera(Camera camera){
        this.camera = camera;
    }
    
    public Camera getCamera(){
        return this.camera;
    }
    public Animator getAnimator(){
        return animator;
    }
    
    public NavMeshAgent Agent{
        get => _agent;
        set => _agent = value;
    }
    public GameObject Weapon{
        get => weapon;
        set => weapon = value;
    }

    public void PlayerToMouseRotation(){
        Vector2 positionOnScreen = camera.WorldToViewportPoint (transform.position);
        Vector2 mouseOnScreen = camera.ScreenToViewportPoint(Input.mousePosition);
        float angle =  Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
        transform.rotation =  Quaternion.Euler (new Vector3(0f,transform.rotation.y-angle-45,0f));
    }

    public void resetStats(){
        isDead = false;
        
        level = WorldManager.instance.config.level;
        maxHealth = WorldManager.instance.config.maxHealth; // Maximum amount of health
        maxMana = WorldManager.instance.config.maxMana;
        health = WorldManager.instance.config.health; // Current amount of health
        mana = WorldManager.instance.config.mana;
        healthRegen = WorldManager.instance.config.healthRegen;
        manaRegen = WorldManager.instance.config.manaRegen;
        if (PlayerPrefs.HasKey("restartGold")){
            gold = PlayerPrefs.GetInt("restartGold");
            PlayerPrefs.DeleteKey("restartGold");
        }
        else{
            gold = WorldManager.instance.config.gold;
        }
        
        goldMultiplier = WorldManager.instance.config.goldMultiplier;
        exp = WorldManager.instance.config.exp;
        expMultiplier = WorldManager.instance.config.expMultiplier;
        movementSpeed = WorldManager.instance.config.movementSpeed;
        resistance = WorldManager.instance.config.resistance;
        lifesteal = WorldManager.instance.config.lifesteal;
        attackDamage = WorldManager.instance.config.attackDamage;
        criticalChance = WorldManager.instance.config.criticalChance;//%
        criticalDamage = WorldManager.instance.config.criticalDamage;//%
        awarenessRange = WorldManager.instance.config.awarenessRange; //Multiplier

        sumExp = WorldManager.instance.config.sumExp;
        needExp = WorldManager.instance.config.needExp;
        killed_mobs = WorldManager.instance.config.killed_mobs;
    }

    public void load_upgrades(){
        if (PlayerPrefs.HasKey("up1") && PlayerPrefs.HasKey("up2") && PlayerPrefs.HasKey("up3") &&
            PlayerPrefs.HasKey("up4")){
            spell_dmg_up = PlayerPrefs.GetInt("up1");
            cooldown_up = PlayerPrefs.GetInt("up2");
            frisbee_up = PlayerPrefs.GetInt("up3");
            slowdown_up = PlayerPrefs.GetInt("up4");
        }
        else{
            spell_dmg_up = 10;
            cooldown_up = 5;
            frisbee_up = 4;
            slowdown_up = 5;
            PlayerPrefs.SetInt("up1", (int)spell_dmg_up);
            PlayerPrefs.SetInt("up2", (int)cooldown_up);
            PlayerPrefs.SetInt("up3", (int)frisbee_up);
            PlayerPrefs.SetInt("up4", (int)slowdown_up);
        }
    }

    public bool notWalkCauseUI(){
        if (LevelUpUpgradesUI.Instance.uiActive || Shop.isEnabled || PauseMenu.gameIsPause){
            return true;
        }

        return false;
    }
}
