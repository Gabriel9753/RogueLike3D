using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockedNewArea : MonoBehaviour{
    public GameObject UI;
    public TextMeshProUGUI text;

    public static UnlockedNewArea instance;
    private void Awake(){
        instance = this;
    }

    public void unlockedNew(){
        StartCoroutine(diasableAfterTime());
    }

    IEnumerator diasableAfterTime(){
        UI.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        UI.SetActive(false);
    }    
    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
    }

    
}
