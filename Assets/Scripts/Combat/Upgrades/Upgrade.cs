using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : ScriptableObject
{
    public string text;
    public Sprite symbol;
    public float value;
    public string type;
    
    public virtual string GetText(){
        return null;
    }

    public virtual void SetText(float var){}
    public virtual Sprite GetImage(){
        return null;
    }

    public virtual float Calculate_rnd_value(){
        return 0f;
    }
}
