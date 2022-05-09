using System.Collections;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

[CreateAssetMenu]
public class DashAbility : Ability{
    public static float dashSpeed = 90.0f;
    public static float dashDistance = 12.0f;
    public static float baseSpeed;

 
    #region Singleton

    public static DashAbility instance;

    void Awake ()
    {
        instance = this;

    }

    #endregion
    public override void Activate(){
        baseSpeed = Player.instance.movementSpeed;
        //Dashing
        if (!Player.instance.isAttacking() && !Player.instance.isHit() && !Player.instance.moveAttack() &&
            !Player.instance.standAttack() && !Player.instance.isCasting()){
            Player.instance._agent.ResetPath();
            Debug.Log("called");
            isReady = false;
            Player.instance.PlayerToMouseRotation();
            float alpha = (float) ((Player.instance.transform.rotation.eulerAngles.y % 360) * Math.PI) / 180;
            Vector3 forward = new Vector3((float) Math.Sin(alpha), 0, (float) Math.Cos(alpha));
            Vector3 newDestination = Player.instance.transform.position + forward * (dashDistance);
            Player.instance._agent.SetDestination(newDestination);
            Player.instance.destination = Player.instance._agent.destination;
            //Player.instance.movementSpeed = dashSpeed;
            Player.instance._agent.speed = dashSpeed;
            Player.instance.animator.Play("Dash");
            Player.instance.animator.SetBool("isRunning", false);
            isActive = true;
            activeTime = StatDictionary.dict[name][0];
        }
        else{
            InterruptAttackToDash();
        }
    }

    public static void SetMovementSpeedToBaseSpeed(){
        Player.instance.movementSpeed = baseSpeed;
    }
    
    public void InterruptAttackToDash(){
        if (Player.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f){
            isReady = false;
            Player.instance._agent.ResetPath();
            Player.instance.animator.SetBool("attackToDash", true);
            Player.instance.animator.Play("Dash");
            Player.instance.animator.SetBool("isRunning", false);
            Player.instance.animator.SetBool("isRunToNormal", false);
            Player.instance.GetComponent<PlayerCombo>().ResetCombo();
            Player.instance.PlayerToMouseRotation();
            float alpha = (float)((Player.instance.transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
            Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
            Vector3 newDestination = Player.instance.transform.position + forward * (dashDistance/1.2f);
            Player.instance._agent.SetDestination(newDestination);
            Player.instance.destination = Player.instance._agent.destination;
            //Player.instance.movementSpeed = dashSpeed;
            Player.instance._agent.speed = dashSpeed;
            isActive = true;
            activeTime = StatDictionary.dict[name][0];
        }
    }
    public override IEnumerator Ready(){
        Activate();
        //Player.instance.movementSpeed -= dashSpeed;
        yield break;
    }

    public override IEnumerator Active(){
        if (activeTime > 0){
            activeTime -= Time.deltaTime;
        }
        else{
            isActive = false;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict[name][1];
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