using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        return Mathf.Round(Random.Range(Player.instance.level*0.7f*100f,Player.instance.level*0.9f*100f)/100f);
    }
}
