using System.Collections;
using UnityEngine;
using System;

[CreateAssetMenu]
public class DashAbility : Ability{
    public static float dashSpeed = 70.0f;
    public static float dashDistance = 20.0f;
    
    public override void Activate(){
        //Dashing
        if (Input.GetKeyDown(key)){
            Player.instance.PlayerToMouseRotation();
            float alpha = (float)((Player.instance.transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
            Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
            Vector3 newDestination = Player.instance.transform.position + forward * (dashDistance);
            playerAgent.SetDestination(newDestination);
            Player.instance.destination = playerAgent.destination;
            Player.instance.agentSpeed = dashSpeed;
            Player.instance.animator.Play("Dash");
            Player.instance.animator.SetBool("isRunning", false);
        }
    }
    public override IEnumerator Ready(){
        isReady = false;
        Activate();
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