using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_gold : MonoBehaviour
{
    public GameObject GoldUI;
    public bool uiActive = true;
    
    public Image gold_image;
    public Text gold_textMesh;

    private int amountGold = 0;
    public int maxGold = 9999999;

    #region Singleton

    public static UI_gold Instance;

    //==============================================================
    // Awake
    //==============================================================
    void Awake(){
        Instance = this;
    }

    #endregion
    // Start is called before the first frame update
    void Start(){
        amountGold = (int)Player.instance.gold;
    }

    // Update is called once per frame
    void Update(){
        amountGold = (int) Player.instance.gold;
        if (amountGold > maxGold){
            amountGold = maxGold;
        }

        String gold_text = amountGold.ToString();
        int leftDigits = 7 - gold_text.Length;
        System.Text.StringBuilder outputText = new System.Text.StringBuilder("0000000");
        for (int i = leftDigits, counter = 0; i < outputText.Length; i++, counter++){
            outputText[i] = gold_text[counter];
        }
        gold_textMesh.text = outputText.ToString();
    }
}
