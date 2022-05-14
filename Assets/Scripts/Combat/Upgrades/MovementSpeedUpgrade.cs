using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MovementSpeedUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Hermes\n \nIncrease your players movement speed by {Mathf.Round(var1*100f)/10f}";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return Mathf.Round(Random.Range(0.1f,0.3f)*100f)/100f;
    }
}
