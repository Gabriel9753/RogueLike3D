using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour{
    public GameObject optionsUI;
    public GameObject startMenuUI;
    //FOR SOUNDS
    public GameObject volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        optionsUI.SetActive(false);
        startMenuUI.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BackButton(){
        optionsUI.SetActive(false);
        startMenuUI.SetActive(true);
    }

    public void SetResoltion(){
        Screen.SetResolution(1920,1080,true, 60);
    }

}
