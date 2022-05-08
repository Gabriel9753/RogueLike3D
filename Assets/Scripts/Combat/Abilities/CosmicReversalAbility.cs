using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CosmicReversalAbility : Ability
{
    public GameObject projectile;
    private float projectileSpeed = 15;

    public override void Activate(){
        camera = Player.instance.camera;
        Debug.Log("1");
        if (Input.GetKeyDown(key)){
            Debug.Log("2");
            Player.instance._agent.ResetPath();
            Player.instance.PlayerToMouseRotation();
            Ray ray =camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, moveMask)){
                Debug.Log("Raycast");
                destination = hit.point;
                InstantiateProjectile(Player.instance.transform);
            }
        }
    }
    void InstantiateProjectile(Transform origin){
        Instantiate(projectile, origin.position, Quaternion.Euler (new Vector3(0f,0f,0f)));
    }
    
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        
    }

    public override IEnumerator Ready(){
        isReady = false;
        
        Activate();
        isActive = true;
        activeTime = StatDictionary.dict["fireball"][0];
        yield break;
    }

    public override IEnumerator Active(){
        if (activeTime > 0){
            activeTime -= Time.deltaTime;
        }
        else{
            isActive = false;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict["fireball"][1];
        }
        yield break;
    }

    public override IEnumerator OnCooldown(){
        if (cooldownTime > 0){
            cooldownTime -= Time.deltaTime;
        }
        else{
            isOnCooldown = false;
            isReady = true;
        }
        yield break;
    }
}
