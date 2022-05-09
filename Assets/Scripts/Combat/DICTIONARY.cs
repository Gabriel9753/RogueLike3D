using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class StatDictionary{
    public static IDictionary<string, float[]> dict = new Dictionary<string, float[]>(){
        //Fireball-Ability
        {"Fireball", new[]{0f, 5f, 10f, 15f}},
        {"Dash", new []{0f, 2f, 0f, 5f}},
        {"MagicOrb", new[]{15f, 5f, 20f, 30f}},
        {"Cosmic", new[]{1.5f, 5f, 10f, 20f}},
        {"Heal", new[]{3f, 30f, 5f, 50f}},
        {"Laserbeam", new[]{5f, 5f, 40f, 50f}},
    };
}

/* Array Indices
 * 0 Active Time
 * 1 Cooldown Time
 * 2 Ability Damage
 * 3 Mana cost
 */