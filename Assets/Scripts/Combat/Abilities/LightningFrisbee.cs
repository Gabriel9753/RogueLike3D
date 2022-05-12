using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningFrisbee : Ability
{
    public GameObject projectile;
    private GameObject projectileObj;
    public float projectileSpeed = 1.1f;
    public float searchRadius = 10;

    private Camera camera;
    private Ray ray;
    private Vector2 positionOnScreen;
    private Vector2 mouseOnScreen;
    private float angle;
    private Quaternion rotation;

    private bool startedSpell;

    private GameObject enemyDestination;
    private bool reachedDestination;
    private Vector3 destination_of_spell;
    private bool foundEnemy;
    private int enemyHitCounter;
    public int maxHitEnemies = 20;
    
    public override void Activate(){
        enemyDestination = new GameObject();
        destination_of_spell = new Vector3();
        startedSpell = false;
        reachedDestination = true;
        foundEnemy = false;
        hit_frisbee_enemies = new List<GameObject>();
        enemyHitCounter = 0;
        
        
        Player.instance._agent.ResetPath();
        Player.instance.PlayerToMouseRotation();
        positionOnScreen = camera.WorldToViewportPoint(Player.instance.transform.position);
        mouseOnScreen = camera.ScreenToViewportPoint(Input.mousePosition);
        angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        rotation = Quaternion.Euler(new Vector3(0f, Player.instance.transform.rotation.y - angle + 225, 0f));
    }
    
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void searchRandomNearEnemy(){
        List<GameObject> nearEnemies = new List<GameObject>();
        //Get all near enemies
        foreach (GameObject enemy in SpawnManager.instance.listEnemiesOnField){
            if (Vector3.Distance(enemy.transform.position, projectileObj.transform.position) < searchRadius){
                nearEnemies.Add(enemy);
            }
        }

        if (nearEnemies.Count > 0){
            int randomIndex = Random.Range(0, nearEnemies.Count);
            enemyDestination = nearEnemies[randomIndex];
            destination_of_spell = enemyDestination.transform.position + Vector3.up * 2;
            reachedDestination = false;
            foundEnemy = true;
        }
        else{
            foundEnemy = false;
        }
    }

    void moveOverTime(){
        if (Vector3.Distance(projectileObj.transform.position, destination_of_spell) > 0.2f){
            Vector3 direction = (destination_of_spell - projectileObj.transform.position).normalized * projectileSpeed;
            projectileObj.transform.position += direction * Time.deltaTime; 
        }
        else{
            reachedDestination = true;
        }
    }
    void makeDamage(){
        if (enemyDestination && Vector3.Distance(projectileObj.transform.position, enemyDestination.transform.position) < 4f){
            activeTime += 0.3f;
            enemyHitCounter =
                enemyDestination.GetComponent<EnemyStats>().damageOverTimeLightningFrisbee(StatDictionary.dict[name][2] + StatDictionary.dict[name][2] * Player.instance.spell_dmg_up/100)
                    ? enemyHitCounter++
                    : enemyHitCounter;
        }
    }

    public override IEnumerator Ready(){
        camera = Player.instance.camera;
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Player.instance.mana >= StatDictionary.dict[name][3] && Physics.Raycast(ray, out hit, 1000, moveMask)){
            destination = hit.point;
            isReady = false;
            Activate();
            Player.instance.animator.Play(name);
            Player.instance.GetComponent<PlayerStats>().consumeMana(StatDictionary.dict[name][3]);
            isActive = true;
            activeTime = StatDictionary.dict[name][0];
        }
        yield break;
    }
    
    

    public override IEnumerator Active(){

        
        if (AbilityHolder.LightningFrisbeeReady){
            AbilityHolder.LightningFrisbeeReady = false;
            Debug.Log(destination);
            projectileObj = Instantiate(projectile, destination, rotation);
            activeTime = StatDictionary.dict[name][0];
            startedSpell = true;
            searchRandomNearEnemy();
        }

        if (startedSpell){
            if (enemyHitCounter == maxHitEnemies + (int)Player.instance.frisbee_up){
                activeTime = 0;
            }
            if (activeTime > 0){
                //moveOverTime();
                projectileObj.transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
                if (reachedDestination && foundEnemy){
                    makeDamage();
                    searchRandomNearEnemy();
                }
                if (!foundEnemy){
                    searchRandomNearEnemy();
                }
                if (!reachedDestination && foundEnemy){
                    moveOverTime();
                }
                activeTime -= Time.deltaTime;
            }
            else{
                isActive = false;
                isOnCooldown = true;
                cooldownTime = StatDictionary.dict[name][1];
                cooldownTime -= StatDictionary.dict[name][1] * Player.instance.cooldown_up/100;
                Skills_menu_in_game.Instance.startCooldownSlider(name, cooldownTime);
                hit_frisbee_enemies.Clear();
                Destroy(projectileObj);
                startedSpell = false;
            }
        }
        yield break;
    }

    public override IEnumerator OnCooldown(){
        if (cooldownTime > 0){
            cooldownTime -= Time.deltaTime;
        }
        else{
            isOnCooldown = false;
            isReady = true;
        }
        yield break;
    }
}
