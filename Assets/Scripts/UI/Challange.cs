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
        Debug.Log(beforeAmount + " " + amount + " " + afterAmount);
        text = beforeAmount + " " + amount + " " + afterAmount;
    }
}