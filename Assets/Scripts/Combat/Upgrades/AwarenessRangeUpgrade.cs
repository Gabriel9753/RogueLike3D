using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        return (float)Math.Round(Random.Range(.1f, .3f), 2);
    }
}
