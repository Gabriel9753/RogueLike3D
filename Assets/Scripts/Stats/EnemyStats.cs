using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour{

    public int level = 0;
    public float exp = 0;
    public float gold;

    public float maxHealth = 200;
    public float health = 50;
    public float damage;
    private Animator _animator;
    private NavMeshAgent _agent;
    
    public float possibilityForKnockback = 85;
    public bool hittibaleWhileAttack = true;
    public bool isDead;
    private bool hittableAgainOverTime = true;

    public void Start(){
        _animator = transform.GetComponent<Animator>();
        _agent = transform.GetComponent<NavMeshAgent>();
        level = Player.instance.level;
        calculateAndSetStats();
        isDead = false;
    }

    public void Update(){
        //TODO: DIE in an "enemy" class
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
            _animator.GetCurrentAnimatorStateInfo(0).IsName("die")){
            Destroy(gameObject);
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
    
    IEnumerator damageOverTimeTimer(){
        hittableAgainOverTime = false;
        yield return new WaitForSecondsRealtime(1);
        hittableAgainOverTime = true;
    }
    
    public void TakeDamage(float damage){
        if (isDead){
            return;
        }
        // Make sure damage doesn't go below 0.
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        // Subtract damage from health
        health -= damage;
        DamageTextManager.instance.DamageCreate(transform.position + new Vector3(0,3,0), damage, 11);
        
        if (health <= 0){
            Die();
        }
        else{
            if (hittibaleWhileAttack){
                if (Random.Range(0, 100) < possibilityForKnockback){
                    _animator.Play("hit");
                    GetComponent<EnemyMovement>().endAttack();
                    _animator.SetBool("isRunning", false);
                    _animator.SetBool("isAttacking", false);
                    _agent.ResetPath();
                    GetComponent<EnemyMovement>().attackReady = true;
                }
            }
        }
    }

    public void Die(){
        isDead = true;
        //Give exp to player
        XP_UI.Instance.addXP(exp);
        
        Player.instance.GetComponent<PlayerStats>().IncreaseGold(gold);
        
        GetComponent<CapsuleCollider>().enabled = false;
        _agent.speed = 0;
        GetComponent<EnemyMovement>().enabled = false;
        _animator.Play("die");
        
        //Tell spawnmanager that this enemy died
        SpawnManager.instance.removeEnemyFromList(gameObject.GetComponent<Enemy>().enemyID);
    }
    
    private void calculateAndSetStats(){
        exp = level * 1.5f + 20;
        health = level * 7 + 50;
        maxHealth = health;

        damage = (float)Math.Pow(level, 1.1) * 3 + 15;
        gold = (float)Math.Pow(level, 1.1) * 2 + 3;
    }

    public float calculateDamage(){
        return damage;
    }
    

}