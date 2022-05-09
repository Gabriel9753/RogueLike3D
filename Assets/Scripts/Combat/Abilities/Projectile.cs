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
                Destroy(gameObject);
                return;
            }
            bool inlist = false;
            List<GameObject> itemsToAdd = new List<GameObject>();
            foreach (GameObject t in ability.hit_enemies) {
                if (t != null) {
                    itemsToAdd.Add(t);
                }
            }
            foreach (var enemy in itemsToAdd){
                if (other.gameObject.GetComponent<Enemy>().enemyID == enemy.GetComponent<Enemy>().enemyID){
                    inlist = true;
                    break;
                }
            }
            if(!inlist){
                itemsToAdd.Add(other.gameObject);
            }
            if (name == "CosmicReversal(Clone)"){
                foreach (var enemy in itemsToAdd){
                    enemy.GetComponent<EnemyStats>().TakeDamage(StatDictionary.dict["Cosmic"][2]);
                    ability.hit_enemies.Remove(enemy);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other){
        ability.hit_enemies.Remove(other.gameObject);
        
    }
}