using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour{
    //For later initializing
    private GameObject weapon;
    private BoxCollider _boxCollider;
    private Animator _animator;
    private NavMeshAgent _agent;
    private new Camera camera;
    private RaycastHit hit;
    public int maxDistance = 70;
    public LayerMask moveMask;
    private Vector3 destination;
    
    private bool checkRunAttack;
    
    
    
    // Called when a script is enabled
    void Start(){
        //Get collider from held weapon
        weapon = Player.instance.weapon;
        _boxCollider = weapon.GetComponent<BoxCollider>();
        _animator = Player.instance.getAnimator();
        _agent = GetComponent<NavMeshAgent>();
        camera = Player.instance.getCamera();
        checkRunAttack = false;
    }
    
    // Called once every frame
    void Update(){
        if(Player.instance.standAttack()) 
            _animator.SetBool("isRunToNormal", false);
        if (checkRunAttack && !Player.instance.moveAttack() && !Player.instance.isDashing()){
            _agent.ResetPath();
            checkRunAttack = false;
        }

            //Right click for attack animation and not running
        if (Input.GetMouseButtonDown(1) && !Player.instance.isRunning() && !Player.instance.isDashing() && !Player.instance.isHit()){
            if (!Player.instance.moveAttack()){
                _agent.ResetPath();
                Player.instance.GetComponent<PlayerCombo>().NormalAttack();
            }
        }

        
        //When running -> other attack animation
        if (Input.GetMouseButtonDown(1) && Player.instance.isRunning() && !Player.instance.isHit()){
            //Dash Attack
            if (_agent && _animator){
                Player.instance.PlayerToMouseRotation();
                _agent.ResetPath();
                _animator.Play("RunAttack");
                float alpha = (float)((transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
                Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
                Vector3 newDestination = transform.position + forward * (3f);
                _agent.SetDestination(newDestination);
                checkRunAttack = true;
            }
        }
    }


    public void startAttack(){
        _boxCollider.enabled = true;
    }

    public void endAttack(){
        _boxCollider.enabled = false;
    }
    
    public void GotHit(float damage){
        _animator.Play("playerHit");
        Player.instance.GetComponent<PlayerStats>().TakeDamage(damage);
        Player.instance.GetComponent<PlayerCombo>().ResetCombo();
    }
    
    
}
