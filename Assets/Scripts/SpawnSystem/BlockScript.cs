using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour{
    public int levelToHide;
    // Update is called once per frame
    private void Start(){
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (Player.instance.level == levelToHide){
            UnlockedNewArea.instance.unlockedNew();
            gameObject.SetActive(false);
        }
    }
}
