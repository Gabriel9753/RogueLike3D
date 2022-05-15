using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossSpawner : MonoBehaviour{
    public GameObject spawnpoint;
    public bool spawnedBoss;
    public List<int> levelRange = new List<int>(2);
    public GameObject Boss;
    public List<GameObject> enemies;
    // Start is called before the first frame update
    void Start(){
        spawnedBoss = false;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            if (!spawnedBoss){
                spawnedBoss = true;
                spawnEnemy();
            }
        }
    }

    private void spawnEnemy(){
        GameObject enemy = Instantiate(Boss, spawnpoint.transform.position, Quaternion.Euler(0, 0, 0));
        calculateStatsAndSet(enemy);
        enemy.GetComponent<BossMovement>().spawnPosition = spawnpoint.transform.position;
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        SpawnManager.instance.listEnemiesOnField.Add(enemy);
    }

    private void calculateStatsAndSet(GameObject enemy){
        float xp = 0;
        float health = 0;
        float damage = 0;
        float gold = 0;

        int enemyLevel;
        //Player level lower than min
        if (Player.instance.level < levelRange[0]){
            enemyLevel = levelRange[0];
        }
        //Player level greater than max
        else if (Player.instance.level > levelRange[1]){
            enemyLevel = (int)(levelRange[1] + Player.instance.level * Random.Range(0.5f, 0.8f));
        }
        //In beginning
        else if (Player.instance.level == 1 || Player.instance.level == 2 || Player.instance.level == 3){
            enemyLevel = Player.instance.level;
        }
        //in between
        else{
            int randomLevel = Random.Range(Player.instance.level - 3, Player.instance.level + 3);
            enemyLevel = randomLevel < levelRange[0] ? levelRange[0]:
                (randomLevel > levelRange[1] ? levelRange[1] : randomLevel);
        }
        
        //calculate xp
        xp = enemyLevel * 6.5f + 20;
        //calculate health
        health = enemyLevel * 9.7f + 30;

        damage = enemyLevel * 2.3f + 6;

        gold = enemyLevel * 5.54f + 3.5f;
        
        enemy.GetComponent<EnemyStats>().setStats(xp, health, damage, gold);
    }
}
