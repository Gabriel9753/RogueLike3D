using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision other){
        print("Projectile Collision with: "+other);
        if (!other.transform.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
