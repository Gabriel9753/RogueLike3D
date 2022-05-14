using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projDestory : MonoBehaviour{
    public bool collisionWithWall = false;
    private float timer;

    private void Start(){
        timer = 0;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Wall")){
            Destroy(gameObject);
        }
    }

    private void Update(){
        timer += Time.deltaTime;

        if (timer >= 9f){
            Destroy(gameObject);
        }
    }
}
