using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;
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
        /*
        key_text_skill_1 = GameObject.Find("SkillsInGame/Skill_1/Key_frame/Key_text").GetComponent<TextMeshProUGUI>();
        key_text_skill_2 = GameObject.Find("SkillsInGame/Skill_2/Key_frame/Key_text").GetComponent<TextMeshProUGUI>();
        key_text_skill_3 = GameObject.Find("SkillsInGame/Skill_3/Key_frame/Key_text").GetComponent<TextMeshProUGUI>();
        key_text_skill_4 = GameObject.Find("SkillsInGame/Skill_4/Key_frame/Key_text").GetComponent<TextMeshProUGUI>();
        skillImage_skill_1 = GameObject.Find("SkillsInGame/Skill_1/Skill_frame/Skill_image").GetComponent<Image>();
        skillImage_skill_2 = GameObject.Find("SkillsInGame/Skill_2/Skill_frame/Skill_image").GetComponent<Image>();
        skillImage_skill_3 = GameObject.Find("SkillsInGame/Skill_3/Skill_frame/Skill_image").GetComponent<Image>();
        skillImage_skill_4 = GameObject.Find("SkillsInGame/Skill_4/Skill_frame/Skill_image").GetComponent<Image>();
        */
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
        }
    }
}
