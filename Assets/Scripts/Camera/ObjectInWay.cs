using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInWay : MonoBehaviour{
    public Material mat;
    public Color col;
    public bool isFadeOut;
    public bool isFadeIn;
    
    
    public void Start(){
        mat = gameObject.GetComponent<Renderer>().material;
        col = mat.color;
    }

    public bool ShowTransparent(){
        if (!isFadeIn){
            StartCoroutine(FadeOutMaterial(0.3f));
            return true;
        }
        return false;
    }

    public bool ShowSolid(){
        if (!isFadeOut){
            StartCoroutine(FadeInMaterial(0.3f));
            return true;
        }
        return false;
    }



    IEnumerator FadeOutMaterial(float fadeSpeed){
        isFadeOut = true;
        Color matColor = mat.color;
        float alphaValue = mat.color.a;
        
        while (mat.color.a > 0f){
            print(mat.color.a);
            alphaValue -= Time.deltaTime / fadeSpeed;
            if (alphaValue >= 0f && alphaValue <= 1f){mat.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);}
            else{
                break;
            }
            yield return null;
        }
        
        mat.color = new Color(matColor.r, matColor.g, matColor.b, 0f);
        isFadeOut = false;
    }

    IEnumerator FadeInMaterial(float fadeSpeed){
        isFadeIn = true;
        Color matColor = mat.color;
        float alphaValue = mat.color.a;

        while (mat.color.a <= 1f && mat.color.a >= 0f){
            print(mat.color.a);
            alphaValue += Time.deltaTime / fadeSpeed;
            if (alphaValue < 1f && alphaValue > 0f){
                mat.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
            }
            else{
                break;
            }
            yield return null;
        }

        mat.color = new Color(matColor.r, matColor.g, matColor.b, 1f);
        isFadeIn = false;
    }
}