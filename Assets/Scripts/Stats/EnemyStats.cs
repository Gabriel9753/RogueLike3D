using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyStats : MonoBehaviour{

    public int level = 0;
    public float exp = 0;
    public float gold;
    
    public float maxHealth = 200;
    public float health = 50;
    public float damage;
    private Animator _animator;
    private NavMeshAgent _agent;
    
    public bool hittibaleWhileAttack = true;
    

    public void Start(){
        _animator = transform.GetComponent<Animator>();
        _agent = transform.GetComponent<NavMeshAgent>();
        level = Player.instance.GetComponent<PlayerStats>().level;
        calculateAndSetStats();
    }

    public void Update(){
        //TODO: DIE in an "enemy" class
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && _animator.GetCurrentAnimatorStateInfo(0).IsName("die")) Destroy(gameObject);
    }

    public void TakeDamage(float damage){
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
                _animator.Play("hit");
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isAttacking", false);
                _agent.ResetPath();
            }
        }
    }

    public void Die(){
        //Give exp to player
        XP_UI.Instance.addXP(exp);
        GetComponent<CapsuleCollider>().enabled = false;
        _animator.Play("die");
        SpawnManager.instance.removeEnemyFromList(gameObject);
    }
    
    private void calculateAndSetStats(){
        exp = level * 1.5f + 20;
        health = level * 7 + 50;
        maxHealth = health;

        damage = (float)Math.Pow(level, 1.15) * 3 + 15;
        gold = (float)Math.Pow(level, 1.15) * 2 + 3;
    }

    public float calculateDamage(){
        return damage;
    }
    

}