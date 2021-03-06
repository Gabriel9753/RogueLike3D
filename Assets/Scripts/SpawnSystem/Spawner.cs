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
    public List<GameObject> specialEnemies;
    public int levelStartToSpawn;
    public bool playerInRange;

    private int tempSpawnRate;

    private bool setTempSpawnRate;

    private bool specialSpawn;

    private bool specialSpawnReady;
    // Start is called before the first frame update
    void Start(){
        specialSpawn = false;
        specialSpawnReady = true;
        spawnReady = true;
        playerInRange = false;
        calculateMinMaxRange();
        setTempSpawnRate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!setTempSpawnRate && Player.instance.level >= levelRange[1] + 1 && Player.instance.level < levelRange[1] + 13){
            tempSpawnRate = spawnRate;
            spawnRate = (int)(spawnRate * 6f);
            setTempSpawnRate = true;
        }

        if (setTempSpawnRate && Player.instance.level >= levelRange[1] + 11 || Player.instance.level > 60){
            spawnRate = tempSpawnRate;
        }
        
        if (Player.instance){
            if (Vector3.Distance(Player.instance.transform.position, transform.position) < 3.3f * radiusX ||
                Vector3.Distance(Player.instance.transform.position, transform.position) < 3.3f * radiusZ){
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
        GameObject enemyToSpawn;
        Vector3 spawnerPosition = transform.position;
        Vector3 randomPosition = new Vector3(spawnerPosition.x + Random.Range(-radiusX, radiusX), spawnerPosition.y, spawnerPosition.z + Random.Range(-radiusZ, radiusZ));
        if (Random.Range(0, 100) < 9 && specialSpawnReady && specialEnemies.Count > 0){
            StartCoroutine(specialSpawnCooldown());
            randomEnemyNumber = Random.Range(0, specialEnemies.Count);
            enemyToSpawn = specialEnemies[randomEnemyNumber];
            specialSpawn = true;
        }
        else{
            enemyToSpawn = enemies[randomEnemyNumber];
        }
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

        if (Player.instance.level >= 80){
            enemyLevel = Random.Range(Player.instance.level - 5, Player.instance.level + 5);
        }
        
        //calculate health
        health = enemyLevel * 10f;
        
        if (specialSpawn){
            enemyLevel += 12;
            specialSpawn = false;
            health = enemyLevel * 10f + 150;
        }
        //calculate xp
        xp = enemyLevel * 9f;


        damage = enemyLevel * 2.4f;

        gold = enemyLevel * 1.8f;
        
        enemy.GetComponent<EnemyStats>().setStats(xp, health, damage, gold);
    }

    IEnumerator spawnCooldown(float cooldown){
        spawnReady = false;
        yield return new WaitForSecondsRealtime(Random.Range(cooldown-1, cooldown+1));
        spawnReady = true;
    }
    
    IEnumerator specialSpawnCooldown(){
        specialSpawnReady = false;
        yield return new WaitForSecondsRealtime(Random.Range(60, 150));
        specialSpawnReady = true;
    }

    public void removeNullEnemiesFromList(){
        spawnedEnemies.RemoveAll(s => s == null);
    }
}
