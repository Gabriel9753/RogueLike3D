using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStats : StateMachineBehaviour{
    public float duration;

    public float abilityDamage;
    public float abilityCriticalRate;
    public float abilityCriticalDamage;

    private String clipName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        String speedMultiplierName = Player.instance.GetSpeedMultiplierName();
        animator.SetFloat(speedMultiplierName, duration);
        
        PlayerStats stats = Player.instance.GetComponent<PlayerStats>();
        stats.addPlayerDamage(abilityDamage);
        stats.addPlayerCriticalChance(abilityCriticalRate);
        stats.addPlayerCriticalDamage(abilityCriticalDamage);
    }
}
