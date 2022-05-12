using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateRooms : MonoBehaviour{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            Player.nearDoor = true;
            Player.nearUpStation = false;
        }
    }
    

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            Player.nearDoor = false;
        }
    }

    private void Update(){
        if (Player.nearDoor && Input.GetKeyDown(KeyCode.E)){
            Player.nearDoor = false;
            SetupWorld.instance.CreateRoom();
            WorldManager.instance.StartRun();
        }
    }
}