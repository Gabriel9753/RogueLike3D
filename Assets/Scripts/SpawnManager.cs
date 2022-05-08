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
    
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (spawn_ready){
            spawnEnemy();   
        }
    }

    private void spawnEnemy(){
        int randomSpawnNumber = Random.Range(0, spawner.Count);
        int randomEnemyNumber = Random.Range(0, enemies.Count);
        Vector3 spawnerPosition = spawner[randomSpawnNumber].transform.position;
        GameObject enemyToSpawn = enemies[randomEnemyNumber];
        //enemyToSpawn.name += "_" + spawnedEnemies;
        spawnedEnemies++;
        enemiesOnField++;
        StartCoroutine(spawnCooldown(spawnRate));
        Instantiate(enemyToSpawn, spawnerPosition, Quaternion.Euler(0, 0, 0));
        enemyToSpawn.GetComponent<NavMeshAgent>().enabled = true;
        listEnemiesOnField.Add(enemyToSpawn);
    }
    
    IEnumerator spawnCooldown(float cooldown){
        spawn_ready = false;
        yield return new WaitForSecondsRealtime(cooldown);
        spawn_ready = true;
    }

    public void removeEnemyFromList(GameObject enemy){
        print(enemy.name);
        int index = 0;
        foreach (var e in listEnemiesOnField){
            if (e.name.Equals(enemy.name)){
                index = listEnemiesOnField.IndexOf(e);
            }
        }
        print(index);
        //listEnemiesOnField.RemoveAt(index);
    }
}
