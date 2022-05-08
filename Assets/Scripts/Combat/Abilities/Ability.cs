using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ability : ScriptableObject{
    public string name;
    public Sprite symbol;
    public float activeTime;
    public float cooldownTime;
    public KeyCode key;
    public bool isReady = true;
    public bool isActive;
    public bool isOnCooldown;
    
    public static Camera camera;
    public static NavMeshAgent playerAgent;
    protected RaycastHit hit;
    protected Vector3 destination;
    public LayerMask moveMask;
    public static Animator playerAnimator;
    
    
    public virtual void Activate(){ }

    public virtual IEnumerator Ready(){
        yield break;
    }

    public virtual IEnumerator Active(){
        yield break;
    }

    public virtual IEnumerator OnCooldown(){

        yield break;
    }
}