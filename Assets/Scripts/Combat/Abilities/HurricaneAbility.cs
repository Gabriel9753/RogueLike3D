using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HurricaneAbility : Ability
{
    public GameObject projectile;
    private GameObject projectileObj;
    public float radius = 10;

    private bool startedSpell;

    public override void Activate(){
        startedSpell = false;
        
        Player.instance._agent.ResetPath();
    }

    void moveOverTime(){
        projectileObj.transform.position = Player.instance.transform.position + Vector3.up * 1.4f;
    }
    void makeDamage(){
        foreach (GameObject enemy in SpawnManager.instance.listEnemiesOnField){
            if (Vector3.Distance(projectileObj.transform.position, enemy.transform.position) < radius){
                enemy.GetComponent<EnemyStats>().damageFireHurricane(StatDictionary.dict[name][2] + StatDictionary.dict[name][2] * Player.instance.spell_dmg_up/100);
            }
        }
    }

    public override IEnumerator Ready(){
        if (Player.instance.mana >= StatDictionary.dict[name][3]){
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

        
        if (AbilityHolder.FireHurricaneReady){
            AbilityHolder.FireHurricaneReady = false;
            projectileObj = Instantiate(projectile, Player.instance.transform.position + Vector3.up * 1.4f, Quaternion.identity);
            activeTime = StatDictionary.dict[name][0];
            startedSpell = true;
        }

        if (startedSpell){
            if (activeTime > 0){
                moveOverTime();
                makeDamage();
                projectileObj.transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
                activeTime -= Time.deltaTime;
            }
            else{
                Debug.Log("DESTROYING");
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
