using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour{
    public float weaponDamage = 10;
    public GameObject holder;

    private void OnTriggerEnter(Collider other){
        // PLAYER HITS ENEMY
        if (other.CompareTag("Enemy")){
            PlayerStats stats = Player.instance.GetComponent<PlayerStats>();
            float damage = Player.instance.GetComponent<PlayerStats>().CalculateDamage(
                instance.criticalChance,
                instance.criticalDamage,
                instance.attackDamage
            );
            other.GetComponent<EnemyStats>().TakeDamage(damage);
        }

        // ENEMY HITS PLAYER
        if (other.CompareTag("Player")){
            float damage = holder.GetComponent<EnemyStats>().calculateDamage();
            instance.GetComponent<PlayerAttack>().GotHit(damage);
        }
    }
}