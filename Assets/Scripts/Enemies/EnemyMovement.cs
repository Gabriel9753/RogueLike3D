using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius = 8f;
    public float speed = 8f;

    Transform target;
    NavMeshAgent agent;
    private Animator _animator;
    public GameObject weapon;
    private BoxCollider _boxCollider;
    void Start(){
        _boxCollider = weapon.GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        target = Player.instance.transform;
        _animator = GetComponent<Animator>();
    }

    void Update (){
        agent.speed = speed; 
        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);
        
        // If inside the radius
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("die") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("swing") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            if (distance <= lookRadius){
                // Move towards the player
                _animator.SetBool("isRunning", true);
                agent.SetDestination(target.position);
                if (distance <= 3){
                    //if player is near -> Attack
                    FaceTarget();
                    agent.ResetPath();
                    _animator.Play("swing");
                }
            }
            else{
                agent.ResetPath();
                _animator.SetBool("isRunning", false);
            }
        }

    }

    // Point towards the player
    void FaceTarget (){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
