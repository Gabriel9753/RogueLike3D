using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour{
    //For leveling stats
    public float baseHealth;
    public float baseExp;
    public float baseDamage;
    public float baseGold;
    
    
    
    public int level = 0;
    public float exp = 0;
    public float gold;
    public string enemyType;
    public float maxHealth = 200;
    
    public float health = 50;
    public float damage;
    private Animator _animator;
    private NavMeshAgent _agent;
    
    public float possibilityForKnockback = 85;
    public bool hittibaleWhileAttack = true;
    public bool isDead;
    private bool hittableAgainOverTime = true;
    private bool hittableAgainOverTimeLightningFrisbee = true;
    private bool hittableAgainOverTimeFireHurricane = true;
    

    public void Start(){
        _animator = transform.GetComponent<Animator>();
        _agent = transform.GetComponent<NavMeshAgent>();
        level = Player.instance.level;
        isDead = false;
        GetComponent<Sounds3D>().Play("Spawn");
    }

    public void Update(){
        //TODO: DIE in an "enemy" class
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
            _animator.GetCurrentAnimatorStateInfo(0).IsName("die")){
            Destroy(gameObject);
        }

        if (GetComponent<Sounds3D>().isPlaying("Hit")){
            GetComponent<Sounds3D>().Stop("Hit");
        }
    }

    public void damageOverTime(float damage){
        if (hittableAgainOverTime){
            bool tempHittable = hittibaleWhileAttack;
            hittibaleWhileAttack = false;
            TakeDamage(damage);
            hittibaleWhileAttack = tempHittable;
            StartCoroutine(damageOverTimeTimer());
        }
    }
    
    public void damageFireHurricane(float damage){
        if (hittableAgainOverTimeFireHurricane){
            bool tempHittable = hittibaleWhileAttack;
            hittibaleWhileAttack = false;
            TakeDamage(damage);
            hittibaleWhileAttack = tempHittable;
            StartCoroutine(damageOverTimeTimerFireHurricane());
        }
    }

    public bool damageOverTimeLightningFrisbee(float damage){
        if (hittableAgainOverTimeLightningFrisbee){
            bool tempHittable = hittibaleWhileAttack;
            hittibaleWhileAttack = false;
            TakeDamage(damage);
            hittibaleWhileAttack = tempHittable;
            StartCoroutine(damageOverTimeTimerLightningFrisbee());
            return true;
        }
        return false;
    }
    
    IEnumerator damageOverTimeTimer(){
        hittableAgainOverTime = false;
        yield return new WaitForSecondsRealtime(1f);
        hittableAgainOverTime = true;
    }    
    IEnumerator damageOverTimeTimerLightningFrisbee(){
        hittableAgainOverTimeLightningFrisbee = false;
        yield return new WaitForSecondsRealtime(0.5f);
        hittableAgainOverTimeLightningFrisbee = true;
    }
    IEnumerator damageOverTimeTimerFireHurricane(){
        hittableAgainOverTimeFireHurricane = false;
        yield return new WaitForSecondsRealtime(1);
        hittableAgainOverTimeFireHurricane = true;
    }   
    
    public void TakeDamage(float damage){
        if (PauseMenu.gameIsPause || LevelUpUpgradesUI.Instance.uiActive){
            return;
        }
        
        
        gameObject.GetComponent<Sounds3D>().Play("Hit");
        if (isDead){
            return;
        }
        // Make sure damage doesn't go below 0.
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        // Subtract damage from health
        health -= damage;
        DamageTextManager.instance.DamageCreate(transform.position, damage, 11);
        
        if (health <= 0){
            Die();
        }
        else if (enemyType == "mage"){
            _animator.Play("hit");
        }
        else if (enemyType == "Boss"){
            if (hittibaleWhileAttack){
                if (Random.Range(0, 100) < possibilityForKnockback){
                    _animator.Play("hit");
                    GetComponent<BossMovement>().endAttack();
                    _animator.SetBool("Run", false);
                    if(_agent.enabled)
                        _agent.ResetPath();
                }
            }
        }
        else{
            if (hittibaleWhileAttack){
                if (Random.Range(0, 100) < possibilityForKnockback){
                    _animator.Play("hit");
                    GetComponent<EnemyMovement>().endAttack();
                    _animator.SetBool("isRunning", false);
                    _animator.SetBool("isAttacking", false);
                    if(_agent.enabled)
                        _agent.ResetPath();
                    GetComponent<EnemyMovement>().attackReady = true;
                }
            }
        }
        
    }


    public void Die(){
        if (enemyType != "Boss"){
            if (GetComponent<EnemyMovement>().weapon != null){
                GetComponent<EnemyMovement>().weapon.SetActive(false);
            }
        }
        Player.instance.killed_mobs++;
        GetComponent<Enemy>().whereToSetMaterial.GetComponent<Renderer>().material = GetComponent<Enemy>().deadMat;
        isDead = true;
        //Give exp to player
        XP_UI.Instance.addXP(exp);
        
        Player.instance.GetComponent<PlayerStats>().IncreaseGold(gold);
        
        GetComponent<CapsuleCollider>().enabled = false;
        _agent.speed = 0;
        if (enemyType == "Boss"){
            GetComponent<BossMovement>().enabled = false;
            Player.instance.spell_dmg_up += Player.instance.spell_dmg_up * 0.01f * level;
        }
        else{
            GetComponent<EnemyMovement>().enabled = false;
        }
        _animator.Play("die");
        
        //Tell spawnmanager that this enemy died
        SpawnManager.instance.removeEnemyFromList(GetComponent<Enemy>().enemyID);
    }
    
    public void setStats(float exp, float health, float damage, float gold){
        this.exp = baseExp +exp;
        this.health = baseHealth + health;
        maxHealth = baseHealth + health;

        this.damage = baseDamage + damage;
        this.gold = baseGold + gold;
    }

    public float calculateDamage(){
        return damage;
    }
    

}