using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu]
public class FireballAbility : Ability{
    public GameObject projectile;
    private float projectileSpeed = 15;

    public override void Activate(){
        if (Input.GetKeyDown(key) && camera && !Player.instance.isHit()){
            playerAgent.ResetPath();
            Player.instance.PlayerToMouseRotation();
            
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, moveMask)){
                destination = hit.point;
                InstantiateProjectile(Player.instance.Weapon.transform);
            }
        }
    }
    void InstantiateProjectile(Transform origin){
        Vector2 positionOnScreen = camera.WorldToViewportPoint (Player.instance.transform.position);
        Vector2 mouseOnScreen = camera.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        Debug.Log(positionOnScreen + " vs "+ mouseOnScreen);
        var projectileObj = Instantiate(projectile, origin.position, Quaternion.Euler (new Vector3(0f,Player.instance.transform.rotation.y-angle+225,0f)));
        projectileObj.GetComponent<Rigidbody>().velocity = (new Vector3(destination.x,origin.position.y,destination.z) - origin.position).normalized * projectileSpeed;
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
