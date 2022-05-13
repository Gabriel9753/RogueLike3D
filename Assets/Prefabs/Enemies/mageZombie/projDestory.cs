using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projDestory : MonoBehaviour{
    public bool collisionWithWall = false;
    private void OnTriggerEnter(Collider other){
        Debug.Log("TRIGGER " + other.name);
        if (other.CompareTag("Wall")){
            collisionWithWall = true;
        }
    }
}
