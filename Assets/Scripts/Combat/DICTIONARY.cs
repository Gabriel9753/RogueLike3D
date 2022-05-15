using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDictionary{
    public static IDictionary<string, float[]> dict = new Dictionary<string, float[]>(){
        //Fireball-Ability
        {"Fireball", new[]{3f, 5f, 12f, 8f}},
        {"Dash", new []{0f, 3.5f, 0f, 3f}},
        {"MagicOrb", new[]{15f, 20f, 5f, 24f}},
        //{"Cosmic", new[]{1.5f, 3f, 10f, 20f}},
        {"Heal", new[]{4f, 20f, 30f, 15f}},
        //{"Laserbeam", new[]{5f, 3f, 40f, 50f}},
        {"LightningFrisbee", new[]{20f, 35f, 2.3f, 30f}},
        {"fireHurricane", new[]{16f, 20f, 6f, 21f}},
        {"RunAttack", new[]{2f, 4f, 1, 8f}}
    };
}

/* Array Indices
 * 0 Active Time
 * 1 Cooldown Time
 * 2 Ability Damage
 * 3 Mana cost
 */