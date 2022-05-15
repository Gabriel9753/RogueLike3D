using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour{
    
    public Vector3 spawnPosition;
    public float lookRadius = 8f;
    public float movementSpeed = 8f;
    public bool attackReady = true;
    public bool attackReadyOnCooldown = true;
    public float fleeRate = 60;
    //private float cooldownAttack = 6;
    public bool reachedSpawn;
    public bool isFleeing = false;
    public bool isStuck = false;
    public bool isChecked = false;

    private bool runAgain;
    private EnemyStats _enemyStats;
    private Vector3 position1;
    private Vector3 position2;

    private Vector3 evadeDestination;
    
    Transform target;
    NavMeshAgent agent;
    private Animator _animator;
    public GameObject weapon;
    private BoxCollider _boxCollider;

    
    
    private Quaternion lookRotation;
    private Vector3 direction;
    void Start(){
        runAgain = true;
        _enemyStats = GetComponent<EnemyStats>();
        reachedSpawn = true;
        _boxCollider = weapon.GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        if (!Player.instance.isDead){
            target = Player.instance.transform;
        }
        else{
            Destroy(gameObject);
        }
        _animator = GetComponent<Animator>();
    }

    void Update (){
        if (isFleeing && Vector3.Distance(transform.position, evadeDestination) > 0.4f){
            _animator.SetBool("isRunning", true);
            agent.destination = evadeDestination;
            agent.speed = movementSpeed + GetComponent<EnemyStats>().level * 0.01f;
        }

        if (isFleeing && Vector3.Distance(transform.position, evadeDestination) <= 0.4f){
            _animator.SetBool("isRunning", false);
            isFleeing = false;
            attackReady = true;
        }
        
        if (_enemyStats.enemyType == "Goblin"){
            if(weapon.GetComponent<BoxCollider>().enabled){
                transform.position += direction * 55 * Time.deltaTime;
                agent.enabled = false;
            }
            else{
                if (!agent.enabled){
                    agent.enabled = true;
                    if (Random.Range(0, 100) < fleeRate){
                        isFleeing = true;
                        attackReady = false;
                        flee();
                    }
                    else{
                        attackReady = true;
                    }
                }
            }
        }
        
        if (_enemyStats.enemyType == "Jumper"){
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("swing")){
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f
                    && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f){
                    transform.position += direction * 6 * Time.deltaTime;
                    agent.enabled = false;
                }
            }
            else{
                if (!agent.enabled){
                    agent.enabled = true;
                    if (Random.Range(0, 100) < fleeRate){
                        isFleeing = true;
                        attackReady = false;
                        flee();
                    }
                    else{
                        attackReady = true;
                    }
                }
            }
        }
        
        if (_enemyStats.enemyType == "Parasite"){
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("swing")){
                if (weapon.GetComponent<BoxCollider>().enabled){
                    transform.position += direction * 14 * Time.deltaTime;
                    agent.enabled = false;
                }
            }
            else{
                if (!agent.enabled){
                    agent.enabled = true;
                    if (Random.Range(0, 100) < fleeRate){
                        isFleeing = true;
                        attackReady = false;
                        flee();
                    }
                    else{
                        attackReady = true;
                    }
                }
            }
        }
        
        if (_enemyStats.enemyType == "Armless"){
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("swing")){
                if (weapon.GetComponent<BoxCollider>().enabled){
                    transform.position += direction * 10 * Time.deltaTime;
                    agent.enabled = false;
                }
            }
            else{
                if (!agent.enabled){
                    agent.enabled = true;
                    if (Random.Range(0, 100) < fleeRate){
                        isFleeing = true;
                        attackReady = false;
                        flee();
                    }
                    else{
                        attackReady = true;
                    }
                }
            }
        }
        
        
        if (!reachedSpawn && agent.isActiveAndEnabled){
            if (Vector3.Distance(transform.position, spawnPosition) < 1f){
                agent.ResetPath();
                reachedSpawn = true;
            }
        }
        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);
        
        if (agent.isActiveAndEnabled && _animator.GetCurrentAnimatorStateInfo(0).IsName("swing") || _animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            agent.speed = 0; 
            agent.ResetPath();
        }
        else{
            if (agent.isActiveAndEnabled){
                agent.speed = movementSpeed + GetComponent<EnemyStats>().level * 0.01f;
            }
        }
        
        
        // If inside the radius
        if (runAgain && agent.isActiveAndEnabled && !_animator.GetCurrentAnimatorStateInfo(0).IsName("die") &&
            !_animator.GetCurrentAnimatorStateInfo(0).IsName("swing") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            if (Vector3.Distance(transform.position, spawnPosition) > 100f){
                agent.ResetPath();
                agent.destination = spawnPosition;
                reachedSpawn = false;
            }
            else if (distance <= lookRadius * Player.instance.awarenessRange && reachedSpawn && !isFleeing){
                if (attackReady && Random.Range(0,100) < 3 && 
                     ((_enemyStats.enemyType == "Goblin" && distance <= Random.Range(7,16))
                     || (_enemyStats.enemyType == "Jumper" && distance <= Random.Range(5,8))
                     || distance <= Random.Range(2,6)
                     )
                     ){
                    //if player is near -> Attack
                    FaceTarget();
                    agent.ResetPath();
                    attackReady = false;
                    _animator.Play("swing");
                }
                else if(attackReady){
                    agent.speed = movementSpeed + GetComponent<EnemyStats>().level * 0.01f; 
                    // Move towards the player
                    _animator.SetBool("isRunning", true);
                    agent.SetDestination(target.position);
                }
            }
        }

        if (isFleeing && !isChecked && !_animator.GetCurrentAnimatorStateInfo(0).IsName("swing")){
            StartCoroutine(checkStuck());
        }
        
        if (isStuck){
            _animator.SetBool("isRunning", false);
            attackReady = true;
            isFleeing = false;
            isStuck = false;
        }

        if (Vector3.Distance(target.position, transform.position) >= lookRadius * Player.instance.awarenessRange){
            _animator.SetBool("isRunning", false);
            StartCoroutine(cooldownToRunAgain());
            attackReady = true;
            isFleeing = false;
            isStuck = false;
        }
    }

    private IEnumerator cooldownToRunAgain(){
        runAgain = false;
        yield return new WaitForSeconds(Random.Range(0.5f, 1.6f));
        runAgain = true;
    }
    
    private IEnumerator checkStuck(){
        isChecked = true;
        isStuck = false;
        Vector3 position1 = transform.position;
        yield return new WaitForSeconds(0.3f);
        Vector3 position2 = transform.position;
        if (Vector3.Distance(position1, position2) < 0.1f){
            isStuck = true;
            isFleeing = false;
        }
        else{
            isStuck = false;
        }
        isChecked = false;
    }
    private void flee(){
        float radius = Random.Range(18f, 30f);
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        agent.speed = movementSpeed + GetComponent<EnemyStats>().level * 0.01f; 
        agent.SetDestination(finalPosition);
        agent.destination = finalPosition;
        evadeDestination = finalPosition;
    }

    // Point towards the player
    void FaceTarget (){
        direction = (target.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);
        transform.rotation = lookRotation;
    }

    void OnDrawGizmosSelected (){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    
    
    public void startAttack()
    {
        _boxCollider.enabled = true;
    }

    public void endAttack()
    {
        _boxCollider.enabled = false;
    }

}
