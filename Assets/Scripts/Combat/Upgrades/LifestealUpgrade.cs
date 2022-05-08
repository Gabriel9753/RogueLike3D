using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class LifestealUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Vampire\n Steel {var1}% more health from your enemies!";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return Random.Range((1+Player.instance.level*2), (30+Player.instance.level*5));
    }
}
