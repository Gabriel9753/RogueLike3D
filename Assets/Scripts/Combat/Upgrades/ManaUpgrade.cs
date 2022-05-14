using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ManaUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Mana expansion\n \n Increase your maximum mana by {Mathf.Round(var1*100f)/100f}.";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return Random.Range((int)(5+Player.instance.level*0.6f), (int)(20+Player.instance.level*0.8f));
    }
}
