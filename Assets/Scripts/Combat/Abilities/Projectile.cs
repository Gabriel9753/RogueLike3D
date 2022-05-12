using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
    public Ability ability;
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Enemy")){
            if (name == "Fireball(Clone)"){
                other.gameObject.GetComponent<EnemyStats>().TakeDamage(StatDictionary.dict["Fireball"][2]);
                if (!ability.fireballs_to_destroy.Contains(gameObject)){
                    Debug.Log("Adding: " + name + " " + gameObject.name);
                    ability.fireballs_to_destroy.Add(gameObject);
                    //ability.collsionObjectPosition.Add(other.transform.position);
                }
                return;
            }
            bool inlist = false;
            List<GameObject> checkItems = new List<GameObject>();
            foreach (GameObject t in ability.hit_enemies) {
                if (t != null) {
                    checkItems.Add(t);
                }
            }
            foreach (var enemy in checkItems){
                if (other.gameObject.GetComponent<Enemy>().enemyID == enemy.GetComponent<Enemy>().enemyID){
                    inlist = true;
                    break;
                }
            }
            if(!inlist){
                ability.hit_enemies.Add(other.gameObject);
            }
            if (name == "CosmicReversal(Clone)"){
                foreach (var enemy in checkItems){
                    enemy.GetComponent<EnemyStats>().TakeDamage(StatDictionary.dict["Cosmic"][2] + StatDictionary.dict[name][2] * Player.instance.spell_dmg_up/100);
                    ability.hit_enemies.Remove(enemy);
                }
            }
        }
        
        
        if (other.CompareTag("Wall")){
            if (name == "Fireball(Clone)"){
                if (!ability.fireballs_to_destroy.Contains(gameObject)){
                    ability.fireballs_to_destroy.Add(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other){
        ability.hit_enemies.Remove(other.gameObject);
    }
}