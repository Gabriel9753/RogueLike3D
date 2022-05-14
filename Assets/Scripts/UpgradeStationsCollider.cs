using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStationsCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            Player.nearUpStation = true;
            Player.nearDoor = false;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            Player.nearUpStation = false;
        }
    }
}
