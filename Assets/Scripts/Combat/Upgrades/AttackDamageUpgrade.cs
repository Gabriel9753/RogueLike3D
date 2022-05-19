using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class AttackDamageUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Tiger Power\n \nIncrease your Attack Damage by {var1} and become stronger!";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return (float)Math.Round(Random.Range(1f,4f),2);
    }
}
