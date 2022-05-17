using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsInGame : MonoBehaviour{

    public GameObject sliderMusic;
    public GameObject sliderVFX;

    public float musicSettings;
    public float VFXSettings;
    public bool isEnabled = false;
    public static OptionsInGame instance;

    private void Awake(){
        instance = this;
        isEnabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("volumeFromSlider")){
            sliderMusic.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volumeFromSlider");
            musicSettings = PlayerPrefs.GetFloat("volumeFromSlider");
        }

        if (PlayerPrefs.HasKey("volumeVFX")){
            sliderVFX.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volumeVFX");
            VFXSettings = PlayerPrefs.GetFloat("volumeVFX");
        }
        else{
            musicSettings = 0.5f;
            VFXSettings = 0.5f;
        }
    }

    public void AdjustVolume(){
        PlayerPrefs.SetFloat("volumeFromSlider", sliderMusic.GetComponent<Slider>().value);
        musicSettings = sliderMusic.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("volumeVFX", sliderVFX.GetComponent<Slider>().value);
        VFXSettings = sliderVFX.GetComponent<Slider>().value;
    }

    public void back(){
        PauseMenu.instance.options.SetActive(false);
        isEnabled = false;
        PauseMenu.instance.pauseMenuUI.SetActive(true);
    }
}
