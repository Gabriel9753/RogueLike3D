using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> _spawner;
    public List<GameObject> listEnemiesOnField;
    //private List<Spawner> _spawners;

    #region Singleton
    public static SpawnManager instance;
    void Awake()
    {
        instance = this;
        listEnemiesOnField = new List<GameObject>();
    }
    #endregion

    public void removeEnemyFromList(int enemyID){
        listEnemiesOnField.RemoveAll(s => s.GetComponent<Enemy>().enemyID == enemyID);
    }

    public void killAllEnemies(){
        foreach (GameObject enemy in listEnemiesOnField){
            Destroy(enemy);
        }
    }
}
