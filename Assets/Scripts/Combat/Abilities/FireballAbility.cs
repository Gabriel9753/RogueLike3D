using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEngine;

[CreateAssetMenu]
public class FireballAbility : Ability{
    public GameObject projectile;
    private GameObject projectileCreated;
    private float projectileSpeed = 20f;

    public GameObject fireExplosion;
    public GameObject fireExplosionCreated;
    public float explosionRange;
    public float explosionDamage;

    private Camera camera;
    Ray ray;
    private Vector2 positionOnScreen;
    private Vector2 mouseOnScreen;
    private float angle;
    private Quaternion rotation;

    private Vector3 moveDirection;

    public override void Activate(){
        Player.instance._agent.ResetPath();
        Player.instance.PlayerToMouseRotation();
        positionOnScreen = camera.WorldToViewportPoint(Player.instance.transform.position);
        mouseOnScreen = camera.ScreenToViewportPoint(Input.mousePosition);
        angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        rotation = Quaternion.Euler(new Vector3(0f, Player.instance.transform.rotation.y - angle + 225, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b){
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void fireballExplosion(Vector3 spawnPosition){
        Vector3 nearestEnemyDistance = new Vector3(0, 0, 0);
        //Get all near enemies
        List<GameObject> checklist = new List<GameObject>();
        foreach (GameObject t in SpawnManager.instance.listEnemiesOnField){
            if (t != null){
                checklist.Add(t);
            }
        }

        foreach (GameObject enemy in checklist){
            if (Vector3.Distance(enemy.transform.position, spawnPosition) < explosionRange){
                if (Vector3.Distance(spawnPosition, enemy.transform.position) <
                    Vector3.Distance(spawnPosition, nearestEnemyDistance)){
                    Debug.Log(enemy.name + " is near!");
                    nearestEnemyDistance = enemy.transform.position;
                }

                enemy.GetComponent<EnemyStats>().TakeDamage(explosionDamage);
            }
        }

        Debug.Log("Distance: " + Vector3.Distance(spawnPosition, nearestEnemyDistance));
        if (Vector3.Distance(spawnPosition, nearestEnemyDistance) < 9f){
            Debug.Log("FIREBALL HIT ENEMY");
            fireExplosionCreated = Instantiate(fireExplosion, nearestEnemyDistance + Vector3.up * 2.5f,
                Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else{
            fireExplosionCreated = Instantiate(fireExplosion, spawnPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
        }

        TimerForAbilities.instance.createdExplosion = true;
        TimerForAbilities.instance.setGameobject(fireExplosionCreated);
    }

    private void check_if_destroy(){
        if (fireballs_to_destroy.Contains(projectileCreated)){
            if (!TimerForAbilities.instance.timerStartedExplosion){
                fireballExplosion(projectileCreated.transform.position);
            }

            fireballs_to_destroy.Remove(projectileCreated);
            Destroy(projectileCreated);
            activeTime = 0;
        }
    }

    public override IEnumerator Ready(){
        camera = Player.instance.camera;
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Player.instance.mana >= StatDictionary.dict[name][3] && Physics.Raycast(ray, out hit, 1000, moveMask)){
            destination = hit.point;
            isReady = false;
            Activate();
            Player.instance.animator.Play(name);
            Player.instance.GetComponent<PlayerStats>().consumeMana(StatDictionary.dict[name][3]);
            isActive = true;
            activeTime = StatDictionary.dict[name][0];
        }

        yield break;
    }

    private void moveFireball(){
        if (projectileCreated){
            projectileCreated.transform.position += moveDirection * Time.deltaTime;    
        }
    }

    public override IEnumerator Active(){
        if (AbilityHolder.fireballAnimationReady){
            AbilityHolder.fireballAnimationReady = false;
            Vector3 castPosition = Player.instance.leftHand.transform.position;
            projectileCreated = Instantiate(projectile, castPosition, rotation);
            fireballs_to_destroy.Clear();
            fireExplosionCreated = null;
            moveDirection = (new Vector3(destination.x, castPosition.y, destination.z) - castPosition).normalized * projectileSpeed;
            //projectileCreated.GetComponent<Rigidbody>().velocity = (new Vector3(destination.x, castPosition.y, destination.z) - castPosition).normalized * projectileSpeed;
        }

        if (activeTime > 0){
            activeTime -= Time.deltaTime;
            moveFireball();
            check_if_destroy();
        }
        else{
            if (projectileCreated){
                fireballExplosion(projectileCreated.transform.position);
                Destroy(projectileCreated);
            }
            isActive = false;
            isOnCooldown = true;
            cooldownTime = StatDictionary.dict[name][1];
            cooldownTime -= StatDictionary.dict[name][1] * Player.instance.cooldown_up/100;
            Skills_menu_in_game.Instance.startCooldownSlider(name, cooldownTime);
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