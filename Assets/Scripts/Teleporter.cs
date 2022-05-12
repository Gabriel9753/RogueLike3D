using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            Player.nearTeleporter = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            Player.nearTeleporter = false;
        }
    }

    private void Update(){
        if (Player.nearTeleporter && !Player.instance.isHit() && Input.GetKeyDown(KeyCode.T)){
            Player.nearTeleporter = false;
            Player.instance.transform.position = SetupWorld.instance.mainRoomEntrance.transform.position;
        }
    }
}
