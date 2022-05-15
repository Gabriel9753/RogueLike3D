using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Challange{
    public int time_seconds;
    public string beforeAmount;
    public string afterAmount;
    public int amount;
    public string amount_type;
    public string text;
    
    public void updateText(int amount){
        text = beforeAmount + " " + amount + " " + afterAmount;
    }
    
    public void updateText(){
        text = beforeAmount  + " " + afterAmount;
    }
}