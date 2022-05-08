using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour{
    private float rot;

    public float forward = 0.01f;

    public float right = 0.01f;

    public float rotInc = 0.3f;
    // Start is called before the first frame update
    void Start(){
        rot = 1;
    }

    // Update is called once per frame
    void Update(){
        transform.localPosition += transform.forward * forward;
        transform.localPosition += transform.right*right;
        transform.rotation =  Quaternion.Euler(new Vector3(0f,transform.rotation.y+rot,0f));
        rot += rotInc;
    }
    
}
