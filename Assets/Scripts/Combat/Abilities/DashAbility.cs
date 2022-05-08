using System.Collections;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

[CreateAssetMenu]
public class DashAbility : Ability{
    public static float dashSpeed = 70.0f;
    public static float dashDistance = 15.0f;
    public static float baseSpeed;

 
    #region Singleton

    public static DashAbility instance;

    void Awake ()
    {
        instance = this;

    }

    #endregion
    public override void Activate(){
        //Dashing
        if (Input.GetKeyDown(key)){
            baseSpeed = Player.instance.movementSpeed;
            Player.instance.PlayerToMouseRotation();
            float alpha = (float)((Player.instance.transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
            Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
            Vector3 newDestination = Player.instance.transform.position + forward * (dashDistance);
            Player.instance._agent.SetDestination(newDestination);
            Player.instance.destination = Player.instance._agent.destination;
            Player.instance.movementSpeed = dashSpeed;
            Player.instance.animator.Play("Dash");
            Player.instance.animator.SetBool("isRunning", false);

        }
    }

    public static void SetMovementSpeedToBaseSpeed(){
        Player.instance.movementSpeed = baseSpeed;
    }
    public static void InterruptAttackToDash(){
        if (Player.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f){
            Player.instance.animator.SetBool("attackToDash", true);
            Player.instance.animator.Play("Dash");
            Player.instance.animator.SetBool("isRunning", false);
            Player.instance.animator.SetBool("isRunToNormal", false);
            Player.instance.animator.SetBool("isNormalAttack", false);
            Player.instance.GetComponent<PlayerCombo>().ResetCombo();
            Player.instance.PlayerToMouseRotation();
            float alpha = (float)((Player.instance.transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
            Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
            Vector3 newDestination = Player.instance.transform.position + forward * (DashAbility.dashDistance);
            Player.instance._agent.SetDestination(newDestination);
            Player.instance.destination = Player.instance._agent.destination;
            Player.instance.movementSpeed = DashAbility.dashSpeed;
        }
    }
    public override IEnumerator Ready(){
        isReady = false;
        Activate();
        //Player.instance.movementSpeed -= dashSpeed;
        isActive = true;
        activeTime = StatDictionary.dict["normal_dash"][0];
        yield break;
    }

    public override IEnumerator Active(){
        if (activeTime > 0){
            activeTime -= Time.deltaTime;
        }
        else{
            isActive = false;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict["normal_dash"][1];
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