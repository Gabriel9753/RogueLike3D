using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            Player.instance._agent.ResetPath();
            Player.instance.GetComponent<NavMeshAgent>().enabled = false;
            Player.nearTeleporter = false;
            Player.instance.transform.position = SetupWorld.instance.mainRoomEntrance.transform.position;
            Player.instance.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
