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
    
    private bool runAttackReady;

    /*
    public GameObject slashVFX1;
    public GameObject slashVFX2;
    public GameObject slashVFX3;
    public GameObject slashVFX4;
    */
    
    
    
    // Called when a script is enabled
    void Start(){
        //Get collider from held weapon
        weapon = Player.instance.weapon;
        _boxCollider = weapon.GetComponent<BoxCollider>();
        _animator = Player.instance.getAnimator();
        _agent = GetComponent<NavMeshAgent>();
        camera = Player.instance.getCamera();
        checkRunAttack = false;
        runAttackReady = true;
    }
    
    // Called once every frame
    void Update(){
        if (Player.instance.standAttack()){
            _animator.SetBool("isRunToNormal", false);
            if (Player.instance.weapon.GetComponent<BoxCollider>().enabled){
                float alpha = (float)((Player.instance.transform.rotation.eulerAngles.y % 360) * Math.PI)/180;
                Vector3 forward = new Vector3((float)Math.Sin(alpha), 0, (float)Math.Cos(alpha));
                Vector3 newDestination = Player.instance.transform.position + forward * (3f);
                Vector3 direction = (newDestination - Player.instance.transform.position).normalized;
                Player.instance.transform.position += direction * 4f * Time.deltaTime;
            }
            
        }
            
        
        if (checkRunAttack && !Player.instance.moveAttack() && !Player.instance.isDashing()){
            _agent.ResetPath();
            checkRunAttack = false;
        }

            //Right click for attack animation and not running
        if (Input.GetMouseButtonDown(1) && !Player.instance.isDashing() && !Player.instance.isHit() && !Player.instance.isCasting()){
            if (!Player.instance.moveAttack()){
                _agent.ResetPath();
                Player.instance.GetComponent<PlayerCombo>().NormalAttack();
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
        Player.instance.GetComponent<PlayerStats>().TakeDamage(damage);
    }

    
}
