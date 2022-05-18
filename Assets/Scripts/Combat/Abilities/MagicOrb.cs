using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MagicOrb : Ability
{
    public GameObject projectile;
    private GameObject projectileObj;
    public float projectileSpeed = 1.1f;

    private Camera camera;
    private Ray ray;
    private Vector2 positionOnScreen;
    private Vector2 mouseOnScreen;
    private float angle;
    private Quaternion rotation;

    private bool startedSpell;
    public override void Activate(){
        startedSpell = false;
        hit_enemies = new List<GameObject>();
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
    

    void enemySlow(){
        foreach (GameObject enemy in SpawnManager.instance.listEnemiesOnField){
            if (Vector3.Distance(enemy.transform.position, projectileObj.transform.position) < 12f + 25 * (Player.instance.slowdown_up / 100)){
                Vector3 direction = (projectileObj.transform.position - enemy.transform.position).normalized * 
                                    (1.55f + 10 * (Player.instance.slowdown_up / 100)) ;
                enemy.transform.position += direction * Time.deltaTime;
            }
        }
    }

    void makeDamage(){
        List<GameObject> itemsToAdd = new List<GameObject>();
        foreach (GameObject t in hit_enemies) {
            if (t != null) {
                itemsToAdd.Add(t);
            }
        }
        foreach(GameObject hit_enemy in itemsToAdd){
            if (hit_enemy == null){
                hit_enemies.Remove(hit_enemy);
            }
            else if (hit_enemy.GetComponent<EnemyStats>().isDead){
                hit_enemies.Remove(hit_enemy);
            }
            else{
                hit_enemy.GetComponent<EnemyStats>().damageOverTime(StatDictionary.dict[name][2] + StatDictionary.dict[name][2] * Player.instance.spell_dmg_up/100);
            }
        }
    }

    public override IEnumerator Ready(){
        camera = Player.instance.camera;
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Player.instance.mana >= StatDictionary.dict[name][3] + Player.instance.level * 1.3f && Physics.Raycast(ray, out hit, 1000, moveMask)){
            destination = hit.point;
            isReady = false;
            Activate();
            Player.instance.animator.Play(name);
            Player.instance.GetComponent<PlayerStats>().consumeMana(StatDictionary.dict[name][3] + Player.instance.level * 1.3f);
            isActive = true;
            activeTime = StatDictionary.dict[name][0];
        }
        yield break;
    }
    
    

    public override IEnumerator Active(){
        if (AbilityHolder.magicOrbAnimationReady){
            AbilityHolder.magicOrbAnimationReady = false;
            Player.instance.GetComponent<Sounds3D>().Play("MagicOrb_cast");
            projectileObj = Instantiate(projectile, destination+Vector3.up*1, rotation);
            activeTime = StatDictionary.dict[name][0];
            startedSpell = true;
        }

        if (startedSpell){
            if (activeTime > 0){
                //moveOverTime();
                projectileObj.transform.Rotate(new Vector3(0, 25, 0) * Time.deltaTime);
                enemySlow();
                makeDamage();
                activeTime -= Time.deltaTime;
            }
            else{
                isActive = false;
                isOnCooldown = true;
                cooldownTime = StatDictionary.dict[name][1];
                cooldownTime -= StatDictionary.dict[name][1] * Player.instance.cooldown_up/100;
                Skills_menu_in_game.Instance.startCooldownSlider(name, cooldownTime);
                hit_enemies.Clear();
                Destroy(projectileObj);
                startedSpell = false;
            }
        }
        else if (!startedSpell && (Player.instance.isHit() || Player.instance.isRunning())){
            isActive = false;
            activeTime = 0;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict[name][1];
            cooldownTime -= StatDictionary.dict[name][1] * Player.instance.cooldown_up/100;
            Skills_menu_in_game.Instance.startCooldownSlider(name, cooldownTime);
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
