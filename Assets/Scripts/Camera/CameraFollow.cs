using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.05f;
    public Vector3 offset;
 
    private Vector3 velocity = Vector3.zero;
    
    public float smoothSpeed = 2f;

    public float currentZoom = 1f;
    public float maxZoom = 3f;
    public float minZoom = .3f;
    public float zoomSensitivity = .7f;

    float zoomSmoothV;
    float targetZoom;


    private void Start(){
        targetZoom = currentZoom;
        target = Player.instance.transform;
    }

    void Update () {
        Vector3 goalPos = target.position + offset;
        transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;

        if (scroll != 0f)
        {
            targetZoom = Mathf.Clamp(targetZoom - scroll, minZoom, maxZoom);
        }
        currentZoom = Mathf.SmoothDamp (currentZoom, targetZoom, ref zoomSmoothV, .15f);
    }

    void LateUpdate () {
        gameObject.GetComponent<Camera>().orthographicSize = currentZoom;
    }

}
