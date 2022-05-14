using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class slashVFX : MonoBehaviour
{
    public List<GameObject> slashesVFX = new List<GameObject>();
    
    //Called by attack animation
    public void startSlashVFX(){
        if(Player.instance.getAnimator().GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_1")){
            Normal1VFX();
        }
        if(Player.instance.getAnimator().GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_2")){
            Normal2VFX();
        }
        if(Player.instance.getAnimator().GetCurrentAnimatorStateInfo(0).IsName("Normal_Attack_3")){
            Normal3VFX();
        }
        if(Player.instance.getAnimator().GetCurrentAnimatorStateInfo(0).IsName("RunAttack")){
            RunAttackVFX();
        }
    }

    public void SlashVFX_3_2(){
        Normal3_2VFX();
    }
    public void SlashVFX_3_3(){
        Normal3_3VFX();
    }

    private void Normal1VFX(){
        GameObject slashVFX = slashesVFX.Where(obj => obj.name == "Normal1VFX").SingleOrDefault();
        StartCoroutine(activationVFX(slashVFX, 0.3f));
    }
    private void Normal2VFX(){
        GameObject slashVFX = slashesVFX.Where(obj => obj.name == "Normal2VFX").SingleOrDefault();
        StartCoroutine(activationVFX(slashVFX, 0.3f));
        
    }
    private void Normal3VFX(){
        GameObject slashVFX = slashesVFX.Where(obj => obj.name == "Normal3VFX").SingleOrDefault();
        StartCoroutine(activationVFX(slashVFX, 0.3f));
        
    }
    
    private void Normal3_2VFX(){
        GameObject slashVFX = slashesVFX.Where(obj => obj.name == "Normal3_2VFX").SingleOrDefault();
        StartCoroutine(activationVFX(slashVFX, 0.3f));
        
    }
    
    private void Normal3_3VFX(){
        GameObject slashVFX = slashesVFX.Where(obj => obj.name == "Normal3_3VFX").SingleOrDefault();
        StartCoroutine(activationVFX(slashVFX, 0.3f));
        
    }
    
    private void RunAttackVFX(){
        GameObject slashVFX = slashesVFX.Where(obj => obj.name == "RunAttackVFX").SingleOrDefault();
        StartCoroutine(activationVFX(slashVFX, 0.3f));
        
    }
    
    IEnumerator activationVFX(GameObject vfx, float time){
        vfx.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        vfx.SetActive(false);
    }
}
