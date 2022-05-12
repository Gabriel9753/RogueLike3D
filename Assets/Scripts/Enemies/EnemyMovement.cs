using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius = 8f;
    public float movementSpeed = 8f;
    public bool attackReady = true;
    public bool attackReadyOnCooldown = true;
    public float fleeRate = 60;
    //private float cooldownAttack = 6;

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
        print("Player ist dead: " + Player.instance.isDead);
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
        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("swing") || _animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            agent.speed = 0;
        }
        else if (slowed){
            agent.speed = movementSpeed - movementSpeed * Player.instance.slowdown_up/100;
        }
        else{
            agent.speed = movementSpeed - (movementSpeed) * 0.15f;
        }
        // If inside the radius
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("die") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("swing") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            if (distance <= lookRadius * Player.instance.awarenessRange){
                if (distance <= 3f && attackReady && Random.Range(0,100) < 3){
                    //if player is near -> Attack
                    FaceTarget();
                    agent.ResetPath();
                    attackReady = false;
                    _animator.Play("swing");
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
                    _animator.SetBool("isRunning", true);
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
                //_animator.SetBool("isRunning", true);
                
            }
        }

        if (isFleeing && !isChecked && !_animator.GetCurrentAnimatorStateInfo(0).IsName("swing")){
            StartCoroutine(checkStuck());
        }
        
        if (isStuck || (Vector3.Distance(target.position, transform.position) >= 8 && Vector3.Distance(agent.destination, transform.position) <= 0.1f)){
            _animator.SetBool("isRunning", false);
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
        //print(Vector3.Distance(position1, position2));
        if (Vector3.Distance(position1, position2) < 0.1f){
            print("YEP HELP ME IM STUCK");
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
        _animator.SetBool("isRunning", true);
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

    public void setSlow(bool slow){
        print(slow + "!!!");
        slowed = slow;
    }
}
