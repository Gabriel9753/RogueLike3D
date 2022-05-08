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

    public float cooldownDash = 1f;
    public bool isDashReady = true;
    
    

    public float walkSpeed = 8.0f;

    private Vector3 startScale;
    public Vector3 dashScale;
    public float shrinkDuration;
    public GameObject dashVFX;

    // Called when a script is enabled
    void Start(){
        animator = Player.instance.getAnimator();
        agent = GetComponent<NavMeshAgent>();
        Player.instance.agentSpeed = walkSpeed;
        startScale = transform.localScale;
    }

    // Called once every frame
    void Update(){
        agent.speed = Player.instance.agentSpeed;
        Debug.Log(agent.speed);
        if (!Player.instance.isDashing() && !Player.instance.isAttacking() && !Player.instance.moveAttack() && !Player.instance.isHit()){
            if (Input.GetMouseButton(0)){
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxDistance, moveMask)){
                    if (Vector3.Distance(hit.point, transform.position) < 0.3f){
                        animator.SetBool("isRunning", false);Player.instance.destination = transform.position;agent.ResetPath();return;}
                    Player.instance.agentSpeed = walkSpeed;
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
            Player.instance.agentSpeed = walkSpeed;
            animator.SetBool("isRunning", false);
        }
        if (Player.instance.standAttack() || Player.instance.isHit()){
            agent.ResetPath();
        }

        if (Player.instance.moveAttack()){
            Player.instance.agentSpeed = 8;
        }
        
        //Dash direct after attacking for fast moving
        if (Player.instance.isAttacking() && isDashReady && !Player.instance.isHit()){
            if (Input.GetKeyDown("space")){
                InterruptAttackToDash();
            }
        }

        if (Player.instance.moveAttack() && isDashReady && !Player.instance.isHit()){
            if (Input.GetKeyDown("space")){
                InterruptAttackToDash();
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
        Player.instance.agentSpeed = speed;
    }

    public void startDash(){
        dashVFX.SetActive(true);
        StartCoroutine(ScaleToTargetCoroutine(dashScale, shrinkDuration));
    }

    public void endDash(){
        StartCoroutine(ScaleToTargetCoroutine(startScale, shrinkDuration));
        dashVFX.SetActive(false);
    }

    private IEnumerator ScaleToTargetCoroutine(Vector3 targetScale, float duration){
        Vector3 startScale = transform.localScale;
        float timer = 0.0f;
        while(timer < duration){
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        yield return null;
    }

    public void InterruptAttackToDash(){
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f){
            animator.SetBool("attackToDash", true);
            animator.Play("Dash");
            animator.SetBool("isRunning", false);
            animator.SetBool("isRunToNormal", false);
            animator.SetBool("isNormalAttack", false);
            Player.instance.GetComponent<PlayerCombo>().ResetCombo();
            Player.instance.PlayerToMouseRotation();
            float alpha = (float)((transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
            Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
            Vector3 newDestination = transform.position + forward * (DashAbility.dashDistance);
            agent.SetDestination(newDestination);
            Player.instance.destination = agent.destination;
            Player.instance.agentSpeed = DashAbility.dashSpeed;
        }
    }
    

}