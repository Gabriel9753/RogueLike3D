using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Transparent : MonoBehaviour{
    [SerializeField] private List<ObjectInWay> currentlyInWay;
    [SerializeField] private List<ObjectInWay> alreadyTransperant;
    private Transform camera;
    private GameObject[] allWalls;

    private void Awake(){
        currentlyInWay = new List<ObjectInWay>();
        alreadyTransperant = new List<ObjectInWay>();
        camera = this.gameObject.transform;
    }

    private void Start(){
        allWalls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in allWalls){
            Color color = wall.GetComponent<Renderer>().material.color;
            color.a = 0;
        }
        
        
    }

    private void Update(){
        GetAllObjectsInWay();
        MakeObjectsTransperant();
        MakeObjectsSolid();
    }

    private void GetAllObjectsInWay(){
        currentlyInWay.Clear();
        float cameraPlayerDistance = Vector3.Magnitude(camera.position - Player.instance.transform.position);

        Ray ray1_Forward = new Ray(camera.position, Player.instance.transform.position - camera.position);
        Ray ray1_Backward = new Ray(Player.instance.transform.position,  camera.position - Player.instance.transform.position );
        var hits1_Forward = Physics.RaycastAll(ray1_Forward, cameraPlayerDistance);
        var hits1_Backward = Physics.RaycastAll(ray1_Backward, cameraPlayerDistance);
             
        foreach (var hit in hits1_Forward){
            if (hit.collider.gameObject.TryGetComponent(out ObjectInWay inWay)){
                if (inWay.gameObject.transform.parent.name.Contains("Archway")){
                    inWay = inWay.gameObject.transform.parent.parent.GetComponent<ObjectInWay>();
                }
                else{
                    inWay = inWay.gameObject.transform.parent.GetComponent<ObjectInWay>();
                }
                if (!currentlyInWay.Contains(inWay)){
                    currentlyInWay.Add(inWay);
                }
            }   
        }

    }

    private void MakeObjectsTransperant(){
        for (int i = 0; i < currentlyInWay.Count; i++){
            ObjectInWay inWay = currentlyInWay[i];
            if (!alreadyTransperant.Contains(inWay)){
                if(inWay.ShowTransparent())
                    alreadyTransperant.Add(inWay);
            }
        }
    }

    private void MakeObjectsSolid(){
        for (int i = alreadyTransperant.Count - 1; i >= 0; i--){
            ObjectInWay wasInWay = alreadyTransperant[i];
            if (!currentlyInWay.Contains(wasInWay)){
                if(wasInWay.ShowSolid())
                    alreadyTransperant.Remove(wasInWay);
            }
        }
    }
}
