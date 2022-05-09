using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour{
    [SerializeField] private List<Ability> held_abilities;
    private List<float> activeTime_abilities = new List<float>();
    private List<float> cooldownTime_abilities = new List<float>();
    private List<Sprite> sprite_abilities = new List<Sprite>();
    private List<KeyCode> key_from_abilities = new List<KeyCode>();
    private List<float> damage_abilities = new List<float>();
    private List<String> name_abilites = new List<string>();
    private float cooldownTime;
    private float activeTime;

    private void Start(){
        foreach (var ability in held_abilities){
            ability.isReady = true;
            ability.cooldownTime = 0;
            activeTime_abilities.Add(StatDictionary.dict[ability.name][0]);
            cooldownTime_abilities.Add(StatDictionary.dict[ability.name][1]);
            damage_abilities.Add(StatDictionary.dict[ability.name][2]);
            key_from_abilities.Add(ability.key);
            sprite_abilities.Add(ability.symbol);
            name_abilites.Add(ability.name);
        }

        Skills_menu_in_game.Instance.setupUI(name_abilites, key_from_abilities, sprite_abilities, cooldownTime_abilities);
    }

    // Update is called once per frame
    void Update(){
        foreach (var ability in held_abilities){
            if (ability.isReady){
                //Ready state
                if (Input.GetKeyDown(ability.key) && !Player.instance.isCasting()){
                    if (ability.name == "Dash"){
                        if (!Player.instance.isHit()){
                            StartCoroutine(ability.Ready());
                        }
                    }
                    else{
                        if (!Player.instance.isAttacking() && !Player.instance.isHit() && !Player.instance.moveAttack() &&
                            !Player.instance.standAttack()){
                            StartCoroutine(ability.Ready());
                        }
                    }
                }
            }
            else if (ability.isActive){
                if (ability){
                    StartCoroutine(ability.Active());
                }
            }
            else if (ability.isOnCooldown){
                StartCoroutine(ability.OnCooldown());
            }
        }
    }
}