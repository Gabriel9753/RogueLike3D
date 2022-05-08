using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour{
    private Animator _animator;
    
    private bool comboPossible;
    public int comboStep;

    public float cooldown_normal1 = 3;
    public bool normal1_ready = true;
    public float cooldown_normal2 = 3;
    public bool normal2_ready = true;
    public float cooldown_normal3 = 3;
    public bool normal3_ready = true;

    void Start(){
        _animator = Player.instance.getAnimator();
    }

    public void ComboPossible(){
        comboPossible = true;
    }

    //Is called by animation for transisition from Attack A -> Attack B. Only if you hit the right timing and incremented comboStep.
    public void NextAttack(){
        if (Player.instance.isHit()){
            ResetCombo();
            return;
        }
        if (comboStep == 1 && normal1_ready){
            Player.instance.PlayerToMouseRotation();
            _animator.Play("Normal_Attack_1");
            StartCoroutine(Cooldown_Normal_1());
        }
        if(comboStep == 2 && normal2_ready){
            
            Player.instance.PlayerToMouseRotation();
            _animator.Play("Normal_Attack_2");
            StartCoroutine(Cooldown_Normal_2());
        }
        if(comboStep == 3 && normal3_ready){
            Player.instance.PlayerToMouseRotation();
            _animator.Play("Normal_Attack_3");
            StartCoroutine(Cooldown_Normal_3());
        }
    }

    public void ResetCombo(){
        comboPossible = false;
        comboStep = 0;
    }

    public void NormalAttack(){
        if (comboStep == 0 && normal1_ready){
            Player.instance.PlayerToMouseRotation();
            _animator.Play("Normal_Attack_1");
            StartCoroutine(Cooldown_Normal_1());
            comboStep = 1;
            return;
        }

        if (comboStep != 0){
            if (comboPossible){
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    IEnumerator Cooldown_Normal_1(){
        normal1_ready = false;
        yield return new WaitForSecondsRealtime(cooldown_normal1);
        normal1_ready = true;
    }
    IEnumerator Cooldown_Normal_2(){
        normal2_ready = false;
        yield return new WaitForSecondsRealtime(cooldown_normal2);
        normal2_ready = true;
    }
    IEnumerator Cooldown_Normal_3(){
        normal3_ready = false;
        yield return new WaitForSecondsRealtime(cooldown_normal3);
        normal3_ready = true;
    }
    
}
