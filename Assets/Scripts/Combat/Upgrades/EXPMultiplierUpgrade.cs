using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class EXPMultiplierUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Gimme more!\n \nGet {Mathf.Round(var1*100f)}% more experience points!";
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
