using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AwarenessRangeUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Awareness Range {var1} ";
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
