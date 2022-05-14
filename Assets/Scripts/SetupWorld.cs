using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SetupWorld : MonoBehaviour{
    public List<GameObject> UIElementsActiveBegin;
    //public GameObject player;
    public static WorldManager worldManager;
    public GameObject player;
    public new Camera camera;
    public static SetupWorld instance;
    
    public GameObject baseRoomEntrance;
    public GameObject mainRoomEntrance;

    public GameObject baseRoom;
    public GameObject mainRoom;
    public NavMeshSurface baseRoomSurface;
    public NavMeshSurface mainRoomSurface;

    public GameObject lastCreatedRoom;

    // Start is called before the first frame update
    void Awake(){
        instance = this;
        setup();
    }

    private void Start(){
        FindObjectOfType<AudioManager>().Play("Base_soundtrack");
    }

    public void setup(bool destroyPlayer=false){
        if (destroyPlayer){
            Destroy(Player.instance.gameObject);
            Destroy(lastCreatedRoom);
        }
        
        lastCreatedRoom = Instantiate(baseRoom, position: new Vector3(0,0, 0), Quaternion.identity);
        Instantiate(player);
        Player.instance.camera = camera;
        if (destroyPlayer){
            CameraFollow.instance.target = Player.instance.transform;
        }
        Player.instance.transform.position = baseRoomEntrance.transform.position;
        WorldManager.instance.startedGame = false;
        foreach (GameObject ui in UIElementsActiveBegin){
            ui.SetActive(true);
        }
    }
    
    public void CreateRoom(){
        Vector3 curPosition = lastCreatedRoom.transform.position;
        Destroy(lastCreatedRoom);
        lastCreatedRoom = Instantiate(mainRoom, position: new Vector3(curPosition.x, curPosition.y, curPosition.z), Quaternion.identity);
        baseRoomSurface.BuildNavMesh();
        mainRoomSurface.BuildNavMesh();
        Vector3 position = mainRoomEntrance.transform.position;
        Player.instance.GetComponent<PlayerMovement>().Warp(position);
    }
    


}
