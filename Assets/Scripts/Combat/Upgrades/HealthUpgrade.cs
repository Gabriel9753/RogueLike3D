using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class HealthUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Life expansion\n \nIncrease your maximum health by {Mathf.Round(var1*100f)/100f}.";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return Random.Range((int)(5+Player.instance.level*0.7f), (int)(40+Player.instance.level*0.9f));
    }
}
