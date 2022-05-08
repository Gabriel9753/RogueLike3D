using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour{
    [SerializeField] private List<Ability> abilities;
    private float cooldownTime;
    private float activeTime;

    // Update is called once per frame
    void Update(){
        foreach (var ability in abilities){
            if (ability.isReady){
                //Ready state
                if (Input.GetKeyDown(ability.key)){
                    StartCoroutine(ability.Ready());
                }
            }
            else if (ability.isActive){
                //Active state
                StartCoroutine(ability.Active());
            }
            else if (ability.isOnCooldown){
                StartCoroutine(ability.OnCooldown());
            }
        }
    }
}