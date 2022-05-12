using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour{
    public GameObject optionsUI;

    public GameObject startMenuUI;
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
}
