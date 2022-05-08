using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class HealthRegenUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"HealthRegen {var1} ";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        float min = 10 + Player.instance.GetComponent<PlayerStats>().level * 2 / 10;
        float max = 30 + Player.instance.GetComponent<PlayerStats>().level * 5/10;
        return Random.Range((float)System.Math.Round(min,2),(float)System.Math.Round(max,2));
    }
}
