using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;

public class Skills_menu_in_game : MonoBehaviour{
    public GameObject Skills_HUD_element;

    //
    //Stuff for skill 1
    //
    public String key_skill_1;
    public float cooldown_skill_1;
    public Image skillImage_skill_1;
    public TextMeshProUGUI key_text_skill_1;
    
    //
    //Stuff for skill 2
    //
    public String key_skill_2;
    public float cooldown_skill_2;
    public Image skillImage_skill_2;
    public TextMeshProUGUI key_text_skill_2;
    
    //
    //Stuff for skill 3
    //
    public String key_skill_3;
    public float cooldown_skill_3;
    public Image skillImage_skill_3;
    public TextMeshProUGUI key_text_skill_3;
    
    //
    //Stuff for skill 4
    //
    public String key_skill_4;
    public float cooldown_skill_4;
    public Image skillImage_skill_4;
    public TextMeshProUGUI key_text_skill_4;
    
    //
    //Stuff for skill dash
    //
    public String key_skill_dash;
    public float cooldown_skill_dash;
    public Image skillImage_skill_dash;
    public TextMeshProUGUI key_text_skill_dash;
    
    //
    //Stuff for skill heal
    //
    public String key_skill_heal;
    public float cooldown_skill_heal;
    public Image skillImage_skill_heal;
    public TextMeshProUGUI key_text_skill_heal;
    #region Singleton
    public static Skills_menu_in_game Instance;
    //==============================================================
    // Awake
    //==============================================================
    void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeKeySkill(int skill_number, String key){
        switch (skill_number){
            case 1:
                key_skill_1 = key;
                key_text_skill_1.text = key;
                break;
            case 2:
                key_skill_2 = key;
                key_text_skill_2.text = key;
                break;
            case 3:
                key_skill_3 = key;
                key_text_skill_3.text = key;
                break;
            case 4:
                key_skill_4 = key;
                key_text_skill_4.text = key;
                break;
            case 5:
                key_skill_dash = key;
                key_text_skill_dash.text = key;
                break;
            case 6:
                key_skill_heal = key;
                key_text_skill_heal.text = key;
                break;
        }
    }
}
