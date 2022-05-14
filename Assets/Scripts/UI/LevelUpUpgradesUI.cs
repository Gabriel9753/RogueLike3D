using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUpgradesUI : MonoBehaviour{
    public GameObject UpgradeMenuUI;
    public bool uiActive = false;

    //
    //Stuff for upgrade 1
    //
    public Image upgrade_image_1;
    public TextMeshProUGUI upgrade_text_1;


    //
    //Stuff for upgrade 2
    //
    public Image upgrade_image_2;
    public TextMeshProUGUI upgrade_text_2;

    //
    //Stuff for upgrade 3
    //
    public Image upgrade_image_3;
    public TextMeshProUGUI upgrade_text_3;

    #region Singleton

    public static LevelUpUpgradesUI Instance;

    //==============================================================
    // Awake
    //==============================================================
    void Awake(){
        Instance = this;
    }

    #endregion

    private void Update(){
        if (XP_UI.Instance.uncompletedUps > 0 && !uiActive && !StatsMenu.instance.statsMenuEnabled && !DieMenu.instance.DieMenuEnabled){
            ActivateUI();
        }
    }

    // Update is called once per frame
    public void UpdateUIAbilities(){
        upgrade_image_1.sprite = UpManager.selected_upgrades[0].GetImage();
        upgrade_text_1.text = UpManager.selected_upgrades[0].GetText();
        upgrade_image_2.sprite = UpManager.selected_upgrades[1].GetImage();
        upgrade_text_2.text = UpManager.selected_upgrades[1].GetText();
        upgrade_image_3.sprite = UpManager.selected_upgrades[2].GetImage();
        upgrade_text_3.text = UpManager.selected_upgrades[2].GetText();
    }

    public void ActivateUI(){
        if(Player.instance.level <= 80){
            UpManager.instance.LevelUpped();
            UpdateUIAbilities();
            uiActive = true;
            Time.timeScale = 0f;
            UpgradeMenuUI.SetActive(true);
            
        }
        else{
            XP_UI.Instance.uncompletedUps--;
        }
        
    }

    public void closeUI(){
        AudioManager.instance.Play("upgrade_select");
        XP_UI.Instance.uncompletedUps--;
        uiActive = false;
        Time.timeScale = 1f;
        UpgradeMenuUI.SetActive(false);
    }
}