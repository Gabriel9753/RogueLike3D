using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerForAbilities : MonoBehaviour{
    public bool createdExplosion;
    private GameObject explosion;
    public bool timerStartedExplosion;
    #region Singleton

    public static TimerForAbilities instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion
    // Start is called before the first frame update
    void Start(){
        createdExplosion = false;
        timerStartedExplosion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (createdExplosion){
            StartCoroutine(TimerDestroyExplosion());
        }        
    }
    
    IEnumerator TimerDestroyExplosion(){
        createdExplosion = false;
        timerStartedExplosion = true;
        yield return new WaitForSecondsRealtime(4f);
        timerStartedExplosion = false;
        Destroy(explosion);
    }

    public void setGameobject(GameObject g){
        explosion = g;
    }
}
