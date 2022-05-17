using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour{

    public float radiusX;
    public float radiusZ;
    public float maxEnemiesToSpawn;
    public List<int> levelRange = new List<int>(2);
    public List<GameObject> spawnedEnemies;
    public int spawnRate;
    public bool spawnReady;
    public List<GameObject> enemies;
    public int levelStartToSpawn;
    public bool playerInRange;
    // Start is called before the first frame update
    void Start(){
        spawnReady = true;
        playerInRange = false;
        calculateMinMaxRange();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player.instance){
            if (Vector3.Distance(Player.instance.transform.position, transform.position) < 2f * radiusX ||
                Vector3.Distance(Player.instance.transform.position, transform.position) < 2f * radiusZ){
                playerInRange = true;
            }
            else{
                playerInRange = false;
            }
        }

        if ((spawnReady) && playerInRange && levelStartToSpawn <= Player.instance.level){
            if (!PauseMenu.gameIsPause && !LevelUpUpgradesUI.Instance.uiActive){
                removeNullEnemiesFromList();
                if (spawnedEnemies.Count <= maxEnemiesToSpawn){
                    spawnEnemy();
                }
            }
        }
    }
    
    private void calculateMinMaxRange(){
        Collider _col = GetComponent<Collider>();
        radiusX = _col.bounds.size.x / 2;
        radiusZ = _col.bounds.size.z / 2;
    }
    
    private void spawnEnemy(){
        int randomEnemyNumber = Random.Range(0, enemies.Count);
        Vector3 spawnerPosition = transform.position;
        Vector3 randomPosition = new Vector3(spawnerPosition.x + Random.Range(-radiusX, radiusX), spawnerPosition.y, spawnerPosition.z + Random.Range(-radiusZ, radiusZ));
        GameObject enemyToSpawn = enemies[randomEnemyNumber];
        StartCoroutine(spawnCooldown(spawnRate));
        GameObject enemy = Instantiate(enemyToSpawn, randomPosition, Quaternion.Euler(0, 0, 0));
        calculateStatsAndSet(enemy);
        enemy.GetComponent<EnemyMovement>().spawnPosition = randomPosition;
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        spawnedEnemies.Add(enemy);
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
            enemyLevel = (int)(levelRange[1] + Player.instance.level * Random.Range(0.2f, 0.5f));
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
        xp = enemyLevel * 2f;
        //calculate health
        health = enemyLevel * 2.6f;

        damage = enemyLevel * 1.8f;

        gold = enemyLevel * 1.3f;
        
        enemy.GetComponent<EnemyStats>().setStats(xp, health, damage, gold);
    }

    IEnumerator spawnCooldown(float cooldown){
        spawnReady = false;
        yield return new WaitForSecondsRealtime(Random.Range(cooldown - 5, cooldown));
        spawnReady = true;
    }

    public void removeNullEnemiesFromList(){
        spawnedEnemies.RemoveAll(s => s == null);
    }
}
