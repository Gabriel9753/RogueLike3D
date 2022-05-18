using System.Collections;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

[CreateAssetMenu]
public class RunAttackAbility:Ability{

    #region Singleton
    public static RunAttackAbility instance;
    private Vector3 direction;
    public float factor;

    void Awake(){
        instance = this;
    }
    #endregion

    public override void Activate(){
        if (!Player.instance.isHit()){
            Player.instance.PlayerToMouseRotation();
            Player.instance._agent.ResetPath();
            isReady = false;
            Player.instance.animator.Play("RunAttack");
            Player.instance.animator.SetBool("isRunning", false);
            isActive = true;
            activeTime = StatDictionary.dict[name][0];
            
            float alpha = (float)((Player.instance.transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
            Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
            Vector3 newDestination = Player.instance.transform.position + forward * (3f);
            direction = (newDestination - Player.instance.transform.position).normalized;

        }
    }

    public override IEnumerator Ready(){
        if (Player.instance.mana >= StatDictionary.dict[name][3] + Player.instance.level * 0.7f){
            Activate();
            Player.instance.GetComponent<PlayerStats>().consumeMana(StatDictionary.dict[name][3] + Player.instance.level * 0.7f);
        }
        yield break;
    }

    private void moveForward(){
        if (Player.instance.weapon.GetComponent<BoxCollider>().enabled){
            Player.instance.transform.position += new Vector3(direction.x, 0, direction.z) * factor * Time.deltaTime;
        }
    }

    public override IEnumerator Active(){
        if (!Player.instance.moveAttack()){
            activeTime = 0;
        }
        if (activeTime > 0){
            moveForward();
            activeTime -= Time.deltaTime;
        }
        else{
            isActive = false;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict[name][1];
            cooldownTime -= StatDictionary.dict[name][1] * Player.instance.cooldown_up / 100;
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

