using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> _spawner;
    public List<GameObject> listEnemiesOnField;
    private List<Spawner> _spawners;

    #region Singleton
    public static SpawnManager instance;
    void Awake()
    {
        instance = this;
        listEnemiesOnField = new List<GameObject>();
    }
    #endregion

    private void Start(){
        foreach (GameObject spawn in _spawner){
            _spawners.Add(spawn.GetComponent<Spawner>());
        }
    }

    public void removeEnemyFromList(int enemyID){
        listEnemiesOnField.RemoveAll(s => s.GetComponent<Enemy>().enemyID == enemyID);
    }

    public void killAllEnemies(){
        foreach (GameObject enemy in listEnemiesOnField){
            Destroy(enemy);
        }
    }
}
