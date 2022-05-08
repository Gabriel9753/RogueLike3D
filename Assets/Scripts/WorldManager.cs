using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : MonoBehaviour
{    
    public GameObject newRoom;
    public NavMeshSurface[] surfaces;
    public GameObject[] rooms;
    public GameObject entrance;
    public GameObject entryRoom;
    private int roomCounter= 0;
    
    public void CreateRoom(){
        int rand = Random.Range(0, rooms.Length);
        Vector3 curPosition = newRoom.transform.position;
        Destroy(newRoom);
        newRoom = Instantiate(rooms[rand], position: new Vector3(curPosition.x, curPosition.y, curPosition.z), Quaternion.identity);
        for (int i = 0; i < surfaces.Length; i++){
            surfaces[i].BuildNavMesh();
        }
        entrance = GameObject.FindWithTag("Entrance");
        Vector3 position = entrance.transform.position;
        roomCounter++;
        Player.instance.GetComponent<PlayerMovement>().Warp(position);
    }
}