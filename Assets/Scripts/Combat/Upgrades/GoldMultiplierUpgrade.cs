using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GoldMultiplierUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Gold digger\n \nGet {Mathf.Round(var1*100f)}% more gold";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return 0.5f;
    }
}
