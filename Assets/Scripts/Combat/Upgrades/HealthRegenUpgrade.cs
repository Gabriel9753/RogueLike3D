using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class HealthRegenUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Passive healer\n \n Increase your regeneration by {Mathf.Round(var1*100f)/100f}HP per second";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        float min = 0.3f;
        float max = 0.6f;
        return Random.Range((float)System.Math.Round(min,2),(float)System.Math.Round(max,2));
    }
}
