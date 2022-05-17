using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

[CreateAssetMenu]
public class ResistanceUpgrade : Upgrade
{
    public override void SetText(float var1){
        value = var1;
        text = $"Tanki thingn\n \n You are {Mathf.Round(var1*100f)/100f}% more robust.";
    }

    public override string GetText(){
        return text;
    }

    public override Sprite GetImage(){
        return symbol;
    }

    public override float Calculate_rnd_value(){
        // S M L Packets
        return Random.Range(3f, 6f);
    }
}
