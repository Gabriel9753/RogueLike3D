using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AttackDamageUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Strength like hulk\n \nIncrease your DMG by: {var1} and become stronger!";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return Random.Range((1+Player.instance.GetComponent<PlayerStats>().level*2), (30+Player.instance.GetComponent<PlayerStats>().level*5));
    }
}
