using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealSpell : Ability
{
    public GameObject projectile;
    private GameObject projectileObj;

    public override void Activate(){
        Camera camera = Player.instance.camera;
        if (!Player.instance.isHit()){
            Player.instance._agent.ResetPath();
            Player.instance.GetComponent<Sounds3D>().Play("Heal");
            projectileObj = Instantiate(projectile, Player.instance.transform.position + (Vector3.up*2f), Quaternion.Euler(0, 0, 0));
            Player.instance.GetComponent<PlayerStats>().Heal(StatDictionary.dict[name][2] + StatDictionary.dict[name][2] * Player.instance.spell_dmg_up/100 * 3);
            Player.instance.GetComponent<PlayerStats>().consumeMana(StatDictionary.dict[name][3] + Player.instance.level);
        }
    }
    void moveOverTime(){
        if (Vector3.Distance(projectileObj.transform.position, Player.instance.transform.position) > 1f){
            projectileObj.transform.position = new Vector3(Player.instance.transform.position.x,
                Player.instance.transform.position.y + 2, Player.instance.transform.position.z);
        }
    }

    public override IEnumerator Ready(){
        if (Player.instance.mana >= StatDictionary.dict[name][3] + Player.instance.level){
            isReady = false;
            //Start Animation
            Player.instance.animator.Play(name);
            //When Animation finished
            Activate();
            isActive = true;
            activeTime = StatDictionary.dict[name][0];
        }

        yield break;
    }
    

    public override IEnumerator Active(){
        if (!Player.instance.isCasting()){
            activeTime = 0;
        }
        if (activeTime > 0){
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
