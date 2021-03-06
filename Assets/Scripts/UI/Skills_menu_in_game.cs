using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skills_menu_in_game : MonoBehaviour{
    public GameObject Skills_HUD_element;

    //
    //Stuff for skill 1
    //
    private String name_1;
    public KeyCode key_skill_1;
    public float cooldown_skill_1;
    public Image skillImage_skill_1;
    public TextMeshProUGUI key_text_skill_1;
    public GameObject slider_skill_1;
    
    //
    //Stuff for skill 2
    //
    private String name_2;
    public KeyCode key_skill_2;
    public float cooldown_skill_2;
    public Image skillImage_skill_2;
    public TextMeshProUGUI key_text_skill_2;
    public GameObject slider_skill_2;
    
    
    //
    //Stuff for skill 3
    //
    private String name_3;
    public KeyCode key_skill_3;
    public float cooldown_skill_3;
    public Image skillImage_skill_3;
    public TextMeshProUGUI key_text_skill_3;
    public GameObject slider_skill_3;
    
    //
    //Stuff for skill 4
    //
    private String name_4;
    public KeyCode key_skill_4;
    public float cooldown_skill_4;
    public Image skillImage_skill_4;
    public TextMeshProUGUI key_text_skill_4;
    public GameObject slider_skill_4;
    
    //
    //Stuff for skill dash
    //
    private String name_dash;
    public KeyCode key_skill_dash;
    public float cooldown_skill_dash;
    public Image skillImage_skill_dash;
    public TextMeshProUGUI key_text_skill_dash;
    public GameObject slider_skill_dash;
    
    //
    //Stuff for skill heal
    //
    private String name_heal;
    public KeyCode key_skill_heal;
    public float cooldown_skill_heal;
    public Image skillImage_skill_heal;
    public TextMeshProUGUI key_text_skill_heal;
    public GameObject slider_skill_heal; 
    //
    //Stuff for RunAttack
    //
    private String name_runAttack;
    public KeyCode key_skill_runAttack;
    public float cooldown_skill_runAttack;
    public Image skillImage_skill_runAttack;
    public TextMeshProUGUI key_text_skill_runAttack;
    public GameObject slider_skill_runAttack;

    public GameObject manaText_1;
    public GameObject manaText_2;
    public GameObject manaText_3;
    public GameObject manaText_4;
    public GameObject manaText_5;
    public GameObject manaText_6;
    
    
    #region Singleton
    public static Skills_menu_in_game Instance;
    //==============================================================
    // Awake
    //==============================================================
    void Awake()
    {
        Instance = this;
        Skills_HUD_element.SetActive(false);
    }
    #endregion


    public void changeKeySkill(int skill_number, KeyCode key){
        switch (skill_number){
            case 1:
                key_skill_1 = key;
                key_text_skill_1.text = ""+key;
                break;
            case 2:
                key_skill_2 = key;
                key_text_skill_2.text = ""+key;
                break;
            case 3:
                key_skill_3 = key;
                key_text_skill_3.text = ""+key;
                break;
            case 4:
                key_skill_4 = key;
                key_text_skill_4.text = ""+key;
                break;
            case 5:
                key_skill_dash = key;
                key_text_skill_dash.text = ""+key;
                break;
            case 6:
                key_skill_heal = key;
                key_text_skill_heal.text = ""+key;
                break;
        }
    }

    public void Update(){
        //Runattack
        manaText_1.GetComponent<Text>().text = "" + Math.Round(StatDictionary.dict["RunAttack"][3] + Player.instance.level * 0.7,0);
        //Fireball
        manaText_2.GetComponent<Text>().text = "" + Math.Round(StatDictionary.dict["Fireball"][3] + Player.instance.level,0);
        //LHurricane
        manaText_3.GetComponent<Text>().text = "" + Math.Round(StatDictionary.dict["fireHurricane"][3] + Player.instance.level,0);
        //Magic orb
        manaText_4.GetComponent<Text>().text = "" + Math.Round(StatDictionary.dict["MagicOrb"][3] + Player.instance.level * 1.3f,0);
        //Lightning frisbee
        manaText_5.GetComponent<Text>().text = "" + Math.Round(StatDictionary.dict["LightningFrisbee"][3] + Player.instance.level,0);
        //Heal
        manaText_6.GetComponent<Text>().text = "" + Math.Round(StatDictionary.dict["Heal"][3] + Player.instance.level,0);
    }

    public void setupUI(List<string> nameAbilites, List<KeyCode> keyFromAbilities, List<Sprite> spriteAbilities, List<float> cooldownTimeAbilities){
        for (int i = 0; i < nameAbilites.Count; i++){
            if(nameAbilites[i] == "RunAttack")
            {
                cooldown_skill_runAttack = cooldownTimeAbilities[i];
                skillImage_skill_runAttack.sprite = spriteAbilities[i];
                key_skill_runAttack = keyFromAbilities[i];
                key_text_skill_runAttack.text = ""+keyFromAbilities[i];
                name_runAttack = nameAbilites[i];
                continue;
            }
            if (nameAbilites[i] == "Dash"){
                cooldown_skill_dash = cooldownTimeAbilities[i];
                skillImage_skill_dash.sprite = spriteAbilities[i];
                key_skill_dash = keyFromAbilities[i];
                key_text_skill_dash.text = ""+keyFromAbilities[i];
                name_dash = nameAbilites[i];
                continue;
            }

            if (nameAbilites[i] == "Heal"){
                cooldown_skill_heal = cooldownTimeAbilities[i];
                skillImage_skill_heal.sprite = spriteAbilities[i];
                key_skill_heal = keyFromAbilities[i];
                key_text_skill_heal.text = ""+keyFromAbilities[i];
                name_heal = nameAbilites[i];
                continue;
            }

            switch (i){
                case 0: 
                    key_skill_1 = keyFromAbilities[i];
                    key_text_skill_1.text = ""+keyFromAbilities[i];
                    cooldown_skill_1 = cooldownTimeAbilities[i];
                    name_1 = nameAbilites[i];
                    skillImage_skill_1.sprite = spriteAbilities[i];
                    break;
                case 1: 
                    key_skill_2 = keyFromAbilities[i];
                    key_text_skill_2.text = ""+keyFromAbilities[i];
                    cooldown_skill_2 = cooldownTimeAbilities[i];
                    name_2 = nameAbilites[i];
                    skillImage_skill_2.sprite = spriteAbilities[i];
                    break;
                case 2: 
                    key_skill_3 = keyFromAbilities[i];
                    key_text_skill_3.text = ""+keyFromAbilities[i];
                    cooldown_skill_3 = cooldownTimeAbilities[i];
                    name_3 = nameAbilites[i];
                    skillImage_skill_3.sprite = spriteAbilities[i];
                    break;
                case 3: 
                    key_skill_4 = keyFromAbilities[i];
                    key_text_skill_4.text = ""+keyFromAbilities[i];
                    cooldown_skill_4 = cooldownTimeAbilities[i];
                    name_4 = nameAbilites[i];
                    skillImage_skill_4.sprite = spriteAbilities[i];
                    break;
            }
            
        }
    }

    public void startCooldownSlider(string name, float cooldownTime){
        if (name == name_1){
            StartCoroutine(SlideAnimation(slider_skill_1, cooldownTime));
        }
        if (name == name_2){
            StartCoroutine(SlideAnimation(slider_skill_2, cooldownTime));
        }
        if (name == name_3){
            StartCoroutine(SlideAnimation(slider_skill_3, cooldownTime));
        }
        if (name == name_4){
            StartCoroutine(SlideAnimation(slider_skill_4, cooldownTime));
        }
        if (name == name_dash){
            StartCoroutine(SlideAnimation(slider_skill_dash, cooldownTime));
        }
        if (name == name_heal){
            StartCoroutine(SlideAnimation(slider_skill_heal, cooldownTime));
        }
        if (name == name_runAttack){
            StartCoroutine(SlideAnimation(slider_skill_runAttack, cooldownTime));
        }
    }
    
    private IEnumerator SlideAnimation(GameObject slider, float duration){
        slider.GetComponent<Slider>().maxValue = duration;
        slider.GetComponent<Slider>().value = duration;
        float timer = duration;
        while(timer > 0){
            timer -= Time.deltaTime;
            if (timer > 0){
                slider.GetComponent<Slider>().value = timer;
            }
            yield return null;
        }
        yield return null;
    }
}
