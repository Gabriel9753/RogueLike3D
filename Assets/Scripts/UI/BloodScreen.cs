using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloodScreen : MonoBehaviour{
    public GameObject Image;
    private Color c;
    public float time;
    public float endAlpha;

    private bool isEnabled;
    public static BloodScreen instance;

    private void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        isEnabled = false;
        c = Image.GetComponent<Image>().color;
        Image.SetActive(false);
    }
    

    public void activateUI(){
        if (!isEnabled){
            isEnabled = true;
            StartCoroutine(PositionChange());
        }
    }

    public IEnumerator PositionChange(){
        Image.SetActive(true);
        c.a = 0;
        Image.GetComponent<Image>().color = c;
        
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;
            c.a += 0.03f;
            print(c.a);
            Image.GetComponent<Image>().color = c;
            yield return null;
        }
        timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;
            c.a -= 0.03f;
            print(c.a);
            Image.GetComponent<Image>().color = c;
            yield return null;
        }
        c.a = 0;
        Image.SetActive(false);
        isEnabled = false;
        yield return null;
    }
}
