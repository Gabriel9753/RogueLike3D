using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ManaRegenUpgrade : Upgrade{
    public override void SetText(float var1){
        value = var1;
        text = $"Passive mana\n \nIncrease your regeneration by {Mathf.Round(var1*100f)/100f} Mana per second";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        float min = 0.7f;
        float max = 1.3f;
        return Random.Range((float)System.Math.Round(min,2),(float)System.Math.Round(max,2));
    }
}