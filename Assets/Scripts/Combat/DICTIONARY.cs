using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class StatDictionary{
    public static IDictionary<string, float[]> dict = new Dictionary<string, float[]>(){
        //Fireball-Ability
        {"fireball",new[]{0f,5f,10f}},
        {"normal_dash",new []{0f,2f,0f}}
    };
}

/* Array Indices
 * 0 Active Time
 * 1 Cooldown Time
 * 2 Ability Damage
 */