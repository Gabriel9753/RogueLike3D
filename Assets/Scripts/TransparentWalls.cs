using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWalls : MonoBehaviour{
    private new Camera camera;
    private Ray ray;
    private RaycastHit hit;
    public float maxDistance;
    public LayerMask moveMask;
    public Material invisMaterial;
    public Transform player;
    public Vector3 offset;
    public Transform[] obstructions;

    private int oldHitsNumber;

    void Start(){
        camera = Player.instance.getCamera();
        oldHitsNumber = 0;
        maxDistance = Vector3.Distance(camera.transform.position, Player.instance.transform.position);

    }

    void LateUpdate(){
        viewObstructed();
    }

    // Update is called once per frame
/*   void Update(){

        ray = new Ray(camera.transform.position, camera.transform.forward);
        //print(camera.transform.position+"/ "+ray.origin +"| "+ Player.instance.transform.position +"/ "+ray.direction );
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        if (Physics.Raycast(ray, out hit, maxDistance, moveMask)){
            print("ray");
            hit.transform.GetComponent<Renderer>().material = invisMaterial;

            transform.position = Player.instance.transform.TransformPoint(offset);
            transform.LookAt(Player.instance.transform);
        }
    }
*/



    void viewObstructed(){
        float characterDistance = Vector3.Distance(camera.transform.position, Player.instance.transform.position);
        int layerNumber = LayerMask.NameToLayer("Wall");
        int layerMask = 1 << layerNumber;
        RaycastHit[] hits = Physics.RaycastAll(camera.transform.position, Player.instance.transform.position - camera.transform.position, characterDistance, layerMask);
        if (hits.Length > 0){
            // Means that some stuff is blocking the view
            int newHits = hits.Length - oldHitsNumber;

            if (obstructions != null && obstructions.Length > 0 && newHits < 0){
                // Repaint all the previous obstructions. Because some of the stuff might be not blocking anymore
                for (int i = 0; i < obstructions.Length; i++){
                    obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
                        UnityEngine.Rendering.ShadowCastingMode.On;
                }
            }

            obstructions = new Transform[hits.Length];
            // Hide the current obstructions
            for (int i = 0; i < hits.Length; i++){
                Transform obstruction = hits[i].transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
                    UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                obstructions[i] = obstruction;
            }

            oldHitsNumber = hits.Length;
        }
        else{
            // Mean that no more stuff is blocking the view and sometimes all the stuff is not blocking as the same time
            if (obstructions != null && obstructions.Length > 0){
                for (int i = 0; i < obstructions.Length; i++){
                    obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
                        UnityEngine.Rendering.ShadowCastingMode.On;
                }

                oldHitsNumber = 0;
                obstructions = null;
            }
        }
    }
}