using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour{
    public int teleportCosts;
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
            if (Player.instance.gold * .05f > 50){
                Player.instance._agent.ResetPath();
                Player.instance.GetComponent<NavMeshAgent>().enabled = false;
                Player.nearTeleporter = false;
                Player.instance.gold -= Player.instance.gold * .05f;
                Player.instance.transform.position = SetupWorld.instance.mainRoomEntrance.transform.position;
                Player.instance.GetComponent<NavMeshAgent>().enabled = true;
            }
            else{
                if (Player.instance.gold >= 50){
                    Player.instance._agent.ResetPath();
                    Player.instance.GetComponent<NavMeshAgent>().enabled = false;
                    Player.nearTeleporter = false;
                    Player.instance.gold -= 50;
                    Player.instance.transform.position = SetupWorld.instance.mainRoomEntrance.transform.position;
                    Player.instance.GetComponent<NavMeshAgent>().enabled = true;
                }
            }

        }
    }
}
