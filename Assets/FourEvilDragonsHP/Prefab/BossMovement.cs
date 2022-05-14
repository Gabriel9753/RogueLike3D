using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
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

    public bool slowed;

    private Vector3 position1;
    private Vector3 position2;

    private Vector3 evadeDestination;
    
    Transform target;
    NavMeshAgent agent;
    private Animator _animator;
    public GameObject weapon;
    private BoxCollider _boxCollider;
    void Start(){
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
        slowed = false;
    }

    void Update (){
        if (!reachedSpawn){
            if (Vector3.Distance(transform.position, spawnPosition) < 2f){
                agent.ResetPath();
                reachedSpawn = true;
            }
        }
        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);
        if (isAttacking() || _animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            agent.speed = 0;
            agent.ResetPath();
        }
        else if (slowed){
            agent.speed = movementSpeed - movementSpeed * Player.instance.slowdown_up/100;
        }
        else{
            agent.speed = movementSpeed - (movementSpeed) * 0.15f;
        }
        // If inside the radius
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("die") && !isAttacking() && !_animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            if (Vector3.Distance(transform.position, spawnPosition) > 110f){
                agent.ResetPath();
                agent.destination = spawnPosition;
                reachedSpawn = false;
            }
            else if (distance <= lookRadius * Player.instance.awarenessRange && reachedSpawn){
                if (distance <= 7.5f && attackReady && Random.Range(0,100) < 15){
                    //if player is near -> Attack
                    FaceTarget();
                    agent.ResetPath();
                    attackReady = false;
                    
                    //Random Attack
                    string[] attacks ={"Basic Attack", "Claw Attack", "Horn Attack"};
                    int randomIndex = Random.Range(0, 3);
                    string randomAttack = attacks[randomIndex];
                    _animator.Play(randomAttack);
                    if (Random.Range(0, 100) < fleeRate){
                        isFleeing = true;
                        flee();
                    }
                    else{
                        attackReady = true;
                    }
                }
                else if(attackReady){
                    agent.speed = movementSpeed; 
                    // Move towards the player
                    _animator.SetBool("Run", true);
                    agent.SetDestination(target.position);
                    if (slowed){
                        agent.speed = movementSpeed - (movementSpeed) * 0.15f;
                    }
                    else{
                        agent.speed = movementSpeed;
                    }
                }
            }
            else{
                _animator.SetBool("Run", true);
            }
        }

        if (isFleeing && !isChecked && !isAttacking()){
            StartCoroutine(checkStuck());
        }
        
        if (isStuck || (Vector3.Distance(target.position, transform.position) >= 8 && Vector3.Distance(agent.destination, transform.position) <= 0.1f)){
            _animator.SetBool("Run", false);
            attackReady = true;
            isFleeing = false;
            isStuck = false;
        }
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
        List<float> randomNumbers = new List<float>();
        randomNumbers.Add(Random.Range(3f,7f));
        randomNumbers.Add(Random.Range(-3f,-7f));
        float random_x = randomNumbers[Random.Range(0,2)];
        float random_z = randomNumbers[Random.Range(0,2)];
        Vector3 evade = new Vector3(target.position.x + random_x, transform.position.y, target.position.z + random_z);
        agent.SetDestination(evade);
        _animator.SetBool("Run", true);
    }

    // Point towards the player
    void FaceTarget (){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
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

    private bool isAttacking(){
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Horn Attack")){
            return true;
        }

        return false;
    }
}
