using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu]
public class CosmicReversalAbility : Ability
{
    public GameObject projectile;
    private float projectileSpeed = 15;
    private GameObject destroyable_projectile;

    public override void Activate(){
        Camera camera = Player.instance.camera;
        if (Input.GetKeyDown(key)){
            Player.instance._agent.ResetPath();
            Player.instance.PlayerToMouseRotation();
            Ray ray =camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, moveMask)){
                InstantiateProjectile(Player.instance.transform);
                destroyable_projectile.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    void InstantiateProjectile(Transform origin){
        destroyable_projectile = Instantiate(projectile, new Vector3(origin.position.x,origin.position.y+1.5f, origin.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
    }

    

    public override IEnumerator Ready(){
        isReady = false;
        
        Activate();
        isActive = true;
        activeTime = StatDictionary.dict[name][0];
        yield break;
    }

    public override IEnumerator Active(){
        if (activeTime < 0.65f){
            Debug.Log("COLLIDERCOLLIDER");
            destroyable_projectile.GetComponent<CapsuleCollider>().enabled = true;
        }
        if (activeTime > 0){
            activeTime -= Time.deltaTime;
        }
        else{
            isActive = false;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict[name][1];
            Skills_menu_in_game.Instance.startCooldownSlider(name, cooldownTime);
        }
        yield break;
    }

    public override IEnumerator OnCooldown(){
        Destroy(destroyable_projectile);
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