using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloodScreen : MonoBehaviour{
    public GameObject Image;
    private Color c;
    public float time;
    public float endAlpha;
    // Start is called before the first frame update
    void Start(){
        c = Image.GetComponent<Image>().color;
        Image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void activateUI(){
        StartCoroutine(PositionChange());
    }

    private IEnumerator PositionChange(){
        Image.SetActive(true);
        c.a = 0;
        Image.GetComponent<Image>().color = c;
        
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;
            c.a += 0.01f;
            print(c.a);
            Image.GetComponent<Image>().color = c;
            yield return null;
        }
        timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;
            c.a -= 0.01f;
            print(c.a);
            Image.GetComponent<Image>().color = c;
            yield return null;
        }
        c.a = 0;
        Image.SetActive(false);
        yield return null;
    }
}
