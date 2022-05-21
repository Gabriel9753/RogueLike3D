using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextManager : MonoBehaviour{
    public GameObject damageText;
    public float subFontSize;
    #region Singleton
    public static DamageTextManager instance;
    private Animator _animator;

    void Awake ()
    {
        instance = this;
    }
    #endregion


    public void DamageCreate(Vector3 position, float num, float length, Color color){
        GameObject dmgTextToSpawn = damageText;
        TextMeshPro textMesh = dmgTextToSpawn.GetComponent<TextMeshPro>();
        textMesh.color = color;
        textMesh.text = Math.Round(num, 0).ToString();
        Vector3 directionToCamera = (Player.instance.camera.transform.position - position + Vector3.up * 2).normalized;
        dmgTextToSpawn = Instantiate(dmgTextToSpawn, (position+Vector3.up*3.3f) + directionToCamera * 5, Quaternion.Euler(30, 45, 0));
        StartCoroutine(PositionChange(dmgTextToSpawn, dmgTextToSpawn.transform.localPosition, 1f));
    }
    
    private IEnumerator PositionChange(GameObject text, Vector3 targetPosition, float duration){
        float rndX = UnityEngine.Random.Range(-1f,1f);
        float rndY = UnityEngine.Random.Range(0f,1f);
        float rndZ = UnityEngine.Random.Range(0f,1f);
        Vector3 startPosition = text.transform.localPosition + (Player.instance.camera.transform.position - text.transform.localPosition).normalized;
        float startSize = text.GetComponent<TextMeshPro>().fontSize;
        targetPosition = targetPosition + new Vector3(rndX, rndY/200, rndZ/10);
        float timer = 0.0f;
        float j = 0;
        while(timer < duration){
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            text.transform.localPosition = Vector3.Lerp(startPosition - new Vector3(0,0,0), targetPosition, t);
            if (text.GetComponent<TextMeshPro>().fontSize > 0)
                text.GetComponent<TextMeshPro>().fontSize -= j + subFontSize;
            if (text.GetComponent<TextMeshPro>().fontSize < 0.2)
                text.GetComponent<TextMeshPro>().fontSize = 0;
            j += 0.00001f;
            yield return null;
        }
        Destroy(text);
        yield return null;
    }
}
