using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDictionary{
    public static IDictionary<string, float[]> dict = new Dictionary<string, float[]>(){
        //Fireball-Ability
        {"Fireball", new[]{3f, 5f, 15f, 8f}},
        {"Dash", new []{3f, 3.5f, 0f, 0f}},
        {"MagicOrb", new[]{15f, 20f, 8f, 22f}},
        //{"Cosmic", new[]{1.5f, 3f, 10f, 20f}},
        {"Heal", new[]{4f, 30f, 55f, 15f}},
        //{"Laserbeam", new[]{5f, 3f, 40f, 50f}},
        {"LightningFrisbee", new[]{25f, 35f, 5f, 27f}},
        {"fireHurricane", new[]{16f, 20f, 6f, 18f}},
        {"RunAttack", new[]{2f, 3f, 2, 5f}}
    };
}

/* Array Indices
 * 0 Active Time
 * 1 Cooldown Time
 * 2 Ability Damage
 * 3 Mana cost
 */