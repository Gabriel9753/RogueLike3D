using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu]
public class GoldUpgrade : Upgrade{
    
    
    public override void SetText(float var1){
        value = var1;
        text = $"Cash payment\n \nGet {var1}g directly!";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return Random.Range((Player.instance.level*2), (Player.instance.level*4))*6f;
    }
}
