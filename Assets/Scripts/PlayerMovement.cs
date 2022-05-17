using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerMovement:MonoBehaviour{
    //private new Camera camera;
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


    private bool footsteps = false;
    // Called when a script is enabled
    void Start(){
        animator = Player.instance.getAnimator();
        agent = GetComponent<NavMeshAgent>();
        startScale = transform.localScale;
        baseSpeed = Player.instance.movementSpeed;
        Player.instance.GetComponent<NavMeshAgent>().enabled = true;
    }

    // Called once every frame
    void Update(){
        if (!Player.instance.isDashing()){
            Player.instance.GetComponent<CapsuleCollider>().enabled = true;
            Player.instance._agent.speed = Player.instance.movementSpeed;
        }
        //agent.speed = Player.instance.movementSpeed;
        if (!Player.instance.isDashing() && !Player.instance.isAttacking() && !Player.instance.moveAttack() && !Player.instance.isHit() && !Player.instance.notWalkCauseUI()){
            if (Input.GetMouseButton(0)){
                Ray ray = Player.instance.camera.ScreenPointToRay(Input.mousePosition);
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
        if (!Player.instance.isDashing() && Vector3.Distance(Player.instance.destination, transform.position) == 0){
            agent.ResetPath();
            animator.SetBool("isRunning", false);
        }
        if (Player.instance.standAttack() || (Player.instance.isHit() && !Player.instance.isDashing())){
            agent.ResetPath();
        }

        if (Player.instance.moveAttack()){
            Player.instance.movementSpeed = baseSpeed;
        }
        
        if (Player.instance.isDashing()){
            animator.SetBool("attackToDash", false);
        }

        if (Player.instance.isRunning() && !Player.instance.GetComponent<Sounds3D>().isPlaying("Footsteps")){
            Player.instance.GetComponent<Sounds3D>().Play("Footsteps");
        }
        if((!Player.instance.isRunning() && Player.instance.GetComponent<Sounds3D>().isPlaying("Footsteps"))
           || PauseMenu.gameIsPause || LevelUpUpgradesUI.Instance.uiActive || Shop.isEnabled || DieMenu.instance.DieMenuEnabled){
            Player.instance.GetComponent<Sounds3D>().Stop("Footsteps");
        }

        if (Input.GetKeyDown(KeyCode.D)){
            Player.instance.animator.Play("Dance");
        }
    }
    
    public void Warp(Vector3 newPosition){
        agent.Warp(newPosition);
        animator.SetBool("isRunning", false);
    }

    public void startDash(){
        dashVFX.SetActive(true);
        Player.instance.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void endDash(){
        DashAbility.SetMovementSpeedToBaseSpeed();
        dashVFX.SetActive(false);
        Player.instance.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void startRoll(){
        Player.instance.isRolling = true;
    }
    public void endRoll(){
        Player.instance.isRolling = false;
    }
}