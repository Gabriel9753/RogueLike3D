using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDictionary{
    public static IDictionary<string, float[]> dict = new Dictionary<string, float[]>(){
        //Fireball-Ability
        {"Fireball", new[]{3f, 12f, 13f, 15f}},
        {"Dash", new []{0f, 3f, 0f, 7f}},
        {"MagicOrb", new[]{12f, 20f, 3f, 35f}},
        {"Cosmic", new[]{1.5f, 3f, 10f, 20f}},
        {"Heal", new[]{4f, 20f, 20f, 30f}},
        {"Laserbeam", new[]{5f, 3f, 40f, 50f}},
        {"LightningFrisbee", new[]{15f, 35f, 4f, 30f}},
        {"fireHurricane", new[]{20f, 15f, 2f, 20f}},
    };
}

/* Array Indices
 * 0 Active Time
 * 1 Cooldown Time
 * 2 Ability Damage
 * 3 Mana cost
 */