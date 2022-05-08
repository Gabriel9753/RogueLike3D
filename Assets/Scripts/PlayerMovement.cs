using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerMovement:MonoBehaviour{
    private new Camera camera;
    public NavMeshAgent agent;
    private RaycastHit hit;
    public static Animator animator;
    public int maxDistance = 70;
    public LayerMask moveMask;
    public float baseSpeed;
    public float cooldownDash = 1f;
    public bool isDashReady = true;
    
    

    

    private Vector3 startScale;
    public Vector3 dashScale;
    public float shrinkDuration;
    public GameObject dashVFX;

    // Called when a script is enabled
    void Start(){
        animator = Player.instance.getAnimator();
        agent = GetComponent<NavMeshAgent>();
        startScale = transform.localScale;
        baseSpeed = Player.instance.movementSpeed;
    }

    // Called once every frame
    void Update(){
        agent.speed = Player.instance.movementSpeed;
        if (!Player.instance.isDashing() && !Player.instance.isAttacking() && !Player.instance.moveAttack() && !Player.instance.isHit()){
            if (Input.GetMouseButton(0)){
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxDistance, moveMask)){
                    if (Vector3.Distance(hit.point, transform.position) < 0.3f){
                        animator.SetBool("isRunning", false);Player.instance.destination = transform.position;agent.ResetPath();return;}
                    animator.SetBool("isRunning", true);
                    agent.SetDestination(hit.point);
                    Player.instance.destination = agent.destination;
                    
                }
            }
        }
        else{
            animator.SetBool("isRunning", false);
        }
        if (Vector3.Distance(Player.instance.destination, transform.position) == 0){
            agent.ResetPath();
            animator.SetBool("isRunning", false);
        }
        if (Player.instance.standAttack() || Player.instance.isHit()){
            agent.ResetPath();
        }

        if (Player.instance.moveAttack()){
            Player.instance.movementSpeed = baseSpeed;
        }
        
        //Dash direct after attacking for fast moving
        if (Player.instance.isAttacking() && isDashReady && !Player.instance.isHit()){
            if (Input.GetKeyDown(DashAbility.instance.key)){
                DashAbility.InterruptAttackToDash();
            }
        }

        if (Player.instance.moveAttack() && isDashReady && !Player.instance.isHit()){
            if (Input.GetKeyDown(DashAbility.instance.key)){
                DashAbility.InterruptAttackToDash();
            }
        }

        if (Player.instance.isDashing()){
            animator.SetBool("attackToDash", false);
        }
    }
    
    public void Warp(Vector3 newPosition){
        agent.Warp(newPosition);
        animator.SetBool("isRunning", false);
    }

    public void setCamera(Camera camera){
        this.camera = camera;
    }

    public void setSpeed(float speed){
        Player.instance.movementSpeed = speed;
    }

    public void startDash(){
        dashVFX.SetActive(true);
    }

    public void endDash(){
        DashAbility.SetMovementSpeedToBaseSpeed();
        dashVFX.SetActive(false);
    }




    

}