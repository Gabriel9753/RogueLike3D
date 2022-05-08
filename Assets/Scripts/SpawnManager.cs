using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawner;

    public List<GameObject> enemies;

    public List<GameObject> listEnemiesOnField;

    public int spawnedEnemies;
    public int enemiesOnField;
    private bool spawn_ready = true;
    public float spawnRate = 10;
    
    #region Singleton
    public static SpawnManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    

    // Update is called once per frame
    void LateUpdate(){
        if (spawn_ready){
            spawn_ready = false;
            spawnEnemy();   
        }
    }

    private void spawnEnemy(){
        int randomSpawnNumber = Random.Range(0, spawner.Count);
        int randomEnemyNumber = Random.Range(0, enemies.Count);
        Vector3 spawnerPosition = spawner[randomSpawnNumber].transform.position;
        GameObject enemyToSpawn = enemies[randomEnemyNumber];
        spawnedEnemies++;
        StartCoroutine(spawnCooldown(spawnRate));
        Instantiate(enemyToSpawn, spawnerPosition, Quaternion.Euler(0, 0, 0));
        enemyToSpawn.GetComponent<NavMeshAgent>().enabled = true;
        print(enemyToSpawn.transform.position);
        listEnemiesOnField.Add(enemyToSpawn);
    }
    
    IEnumerator spawnCooldown(float cooldown){
        yield return new WaitForSecondsRealtime(cooldown);
        spawn_ready = true;
    }

    public void removeEnemyFromList(int enemyID){
        print(enemyID);
        int index = 0;
        foreach (var e in listEnemiesOnField){
            if (e.GetComponent<Enemy>().enemyID == enemyID){
                index = listEnemiesOnField.IndexOf(e);
            }
        }
        print(index);
        listEnemiesOnField.RemoveAt(index);
    }
}
