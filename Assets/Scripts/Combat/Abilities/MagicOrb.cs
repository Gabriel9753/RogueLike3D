using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MagicOrb : Ability
{
    public GameObject projectile;
    private GameObject projectileObj;
    private float projectileSpeed = 1.1f;

    public override void Activate(){
        hit_enemies = new List<GameObject>();
        Camera camera = Player.instance.camera;
        if (!Player.instance.isHit()){
            Player.instance._agent.ResetPath();
            Player.instance.PlayerToMouseRotation();
            //START CAST ANIMATION
            
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, moveMask)){
                destination = hit.point;
                InstantiateProjectile(destination);
            }
        }
    }
    void InstantiateProjectile(Vector3 origin){
        Camera camera = Player.instance.camera;
        Vector2 positionOnScreen = camera.WorldToViewportPoint (Player.instance.transform.position);
        Vector2 mouseOnScreen = camera.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        projectileObj = Instantiate(projectile, origin, Quaternion.Euler (new Vector3(0f,Player.instance.transform.rotation.y-angle+225,0f)));
        //projectileObj.GetComponent<Rigidbody>().velocity = (new Vector3(destination.x,origin.position.y,destination.z) - origin.position).normalized * projectileSpeed;
    }
    
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void moveOverTime(){
        

        if (Vector3.Distance(projectileObj.transform.position, Player.instance.transform.position) > 9){
            Vector3 playerPosition = Player.instance.transform.position + new Vector3(0, 0.5f, 0);
            Vector3 direction = (playerPosition - projectileObj.transform.position).normalized * 3f;
            projectileObj.transform.position += direction * Time.deltaTime; 
        }
        else if(Vector3.Distance(projectileObj.transform.position, Player.instance.transform.position) < 7){
            projectileObj.transform.position += Vector3.forward * Time.deltaTime * 5f;
        }
    }

    void enemySlow(){
        foreach (var enemy in SpawnManager.instance.listEnemiesOnField){
            if (Vector3.Distance(enemy.transform.position, projectileObj.transform.position) < 14f){
                Vector3 direction = (projectileObj.transform.position - enemy.transform.position).normalized * 5f;
                enemy.transform.position += direction * Time.deltaTime;
            }
        }
    }

    void makeDamage(){
        List<GameObject> itemsToAdd = new List<GameObject>();
        foreach (GameObject t in hit_enemies) {
            if (t != null) {
                itemsToAdd.Add(t);
            }
        }
        foreach(GameObject hit_enemy in itemsToAdd){
            if (hit_enemy == null){
                hit_enemies.Remove(hit_enemy);
            }
            else if (hit_enemy.GetComponent<EnemyStats>().isDead){
                hit_enemies.Remove(hit_enemy);
            }
            else{
                hit_enemy.GetComponent<EnemyStats>().damageOverTime(StatDictionary.dict[name][2]);
            }
        }
    }

    public override IEnumerator Ready(){
        isReady = false;
        //Start Animation
        Player.instance.animator.Play(name);
        //When Animation finished
        Activate();
        isActive = true;
        activeTime = StatDictionary.dict[name][0];
        yield break;
    }
    
    

    public override IEnumerator Active(){
        if (activeTime > 0){
            //moveOverTime();
            projectileObj.transform.Rotate(new Vector3(0, 25, 0) * Time.deltaTime);
            enemySlow();
            makeDamage();
            activeTime -= Time.deltaTime;
        }
        else{
            Debug.Log("DESTROYING");
            isActive = false;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict[name][1];
            Skills_menu_in_game.Instance.startCooldownSlider(name, cooldownTime);
            hit_enemies.Clear();
            Destroy(projectileObj);
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
