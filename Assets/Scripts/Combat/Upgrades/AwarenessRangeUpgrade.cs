using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AwarenessRangeUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Magnet\n \nIncrease the awareness range of your enemies by {var1}% so you can better lure!";
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
