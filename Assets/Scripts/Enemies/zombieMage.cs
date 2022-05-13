using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class zombieMage : MonoBehaviour
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
    public float cooldownCasting;
    public bool castReady;
    public float timeCreateProjectile;
    public GameObject createdProjectile;
    public GameObject projectile;
    private Vector3 projectileDirection;
    public GameObject explosionVFX;
    private GameObject createdExplosion;
    public float explosionTime;
    public float timerCreatedProjectile = -1;
    private bool createdAProjetile;
    
    private Vector3 position1;
    private Vector3 position2;

    private Vector3 evadeDestination;
    
    Transform target;
    NavMeshAgent agent;
    private Animator _animator;
    public GameObject rHand;
    private BoxCollider _boxCollider;
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        if (!Player.instance.isDead){
            target = Player.instance.transform;
        }
        else{
            Destroy(gameObject);
        }
        _animator = GetComponent<Animator>();
        slowed = false;
        createdAProjetile = false;
        cooldownCasting = 10f;
    }

    void Update (){
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("die")){
            createdAProjetile = false;
            Destroy(createdProjectile);
        }
        if (timerCreatedProjectile >= 0){
            timerCreatedProjectile += Time.deltaTime;
        }
        if (createdAProjetile){
            if(createdProjectile.GetComponent<projDestory>().collisionWithWall)
            {
                Destroy(createdProjectile);
                createdAProjetile = false;
            }
        }
        
        if (timerCreatedProjectile > 8 && createdAProjetile){
            Destroy(createdProjectile);
            createdAProjetile = false;
        }

        if (createdAProjetile){
            projectileMovement();
        }
        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("casting") || _animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            agent.ResetPath();
            agent.speed = 0;
        }
        // If inside the radius
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("die") &&
            !_animator.GetCurrentAnimatorStateInfo(0).IsName("casting") &&
            !_animator.GetCurrentAnimatorStateInfo(0).IsName("hit") && castReady){
            if (distance <= 25f * Player.instance.awarenessRange){
                if (distance <= 25f && Random.Range(0, 100) < 2){
                    //if player is near -> Attack
                    FaceTarget();
                    _animator.Play("casting");
                    StartCoroutine(cooldownCastTimer());
                    StartCoroutine(startCreatingProjectile());
                    if (Random.Range(0, 100) < fleeRate){
                        isFleeing = true;
                        //flee();
                    }
                }
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

    
    private IEnumerator startCreatingProjectile(){
        yield return new WaitForSecondsRealtime(timeCreateProjectile);
        if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("hit") )
            createProjectile();
    }

    private void createProjectile(){
        createdProjectile = Instantiate(projectile, rHand.transform.position, Quaternion.identity);
        createdProjectile.transform.position = new Vector3(createdProjectile.transform.position.x, Player.instance.transform.position.y + 1.5f, createdProjectile.transform.position.z);
        createdAProjetile = true;
        timerCreatedProjectile = 0;
        projectileMovement();
        projectileDirection = (Player.instance.transform.position + Vector3.up * 1.5f - createdProjectile.transform.position).normalized * 5f;
    }

    private void projectileMovement(){
        createdProjectile.transform.position += projectileDirection * Time.deltaTime;
        createdProjectile.transform.Rotate(new Vector3(0, 70, 0) * Time.deltaTime);
        if (Vector3.Distance(Player.instance.transform.position, createdProjectile.transform.position) < 2.1f){
            createdAProjetile = false;
            startExplode();
        }
    }

    private void startExplode(){
        Destroy(createdProjectile);
        createdExplosion = Instantiate(explosionVFX, Player.instance.transform.position+ Vector3.up * 1.5f, quaternion.identity);
        Player.instance.GetComponent<PlayerStats>().TakeDamage(GetComponent<EnemyStats>().damage * 0.7f);
        StartCoroutine(deleteExplosion());
    }

    private IEnumerator deleteExplosion(){
        yield return new WaitForSecondsRealtime(explosionTime);
        Destroy(createdExplosion);
    }
    
    private IEnumerator cooldownCastTimer(){
        castReady = false;
        yield return new WaitForSecondsRealtime(cooldownCasting);
        castReady = true;
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
        slowed = slow;
    }
    
    
}
