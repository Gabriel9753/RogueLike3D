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
    }

    private void Normal1VFX(){;
        GameObject slashVFX = slashesVFX.Where(obj => obj.name == "Normal1VFX").SingleOrDefault();
        StartCoroutine(activationVFX(slashVFX, 0.3f));
    }
    private void Normal2VFX(){
        
    }
    private void Normal3VFX(){
        
    }
    
    IEnumerator activationVFX(GameObject vfx, float time){
        vfx.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        vfx.SetActive(false);
    }
}
