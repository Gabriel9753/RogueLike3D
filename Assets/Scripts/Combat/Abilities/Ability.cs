using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ability : ScriptableObject{
    public new string name;
    public Sprite symbol;
    public float activeTime;
    public float cooldownTime;
    public KeyCode key;
    public bool isReady = true;
    public bool isActive;
    public bool isOnCooldown;
    
    //public static Camera camera;
    protected RaycastHit hit;
    protected Vector3 destination;
    public LayerMask moveMask;
    public static Animator playerAnimator;
    public List<GameObject> hit_enemies;
    public List<GameObject> hit_frisbee_enemies;
    
    public List<GameObject> fireballs_to_destroy;

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