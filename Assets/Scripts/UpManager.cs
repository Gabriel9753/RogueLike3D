using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UpManager : MonoBehaviour{
    [SerializeField] private List<Upgrade> all_upgrades;
    public static List<Upgrade> selected_upgrades;

    #region Singleton

    public static UpManager instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion
    // Update is called once per frame
    public void LevelUpped(){
        selected_upgrades = Select_random_upgrades();
        foreach (var upgrade in selected_upgrades){
            upgrade.SetText(upgrade.Calculate_rnd_value());
        }
    }


    public List<Upgrade> Select_random_upgrades(){
        List<Upgrade> result = new List<Upgrade>();
        for (int i = 0; i < 3; i++){
            int idx = Random.Range(0, all_upgrades.Count);
            result.Add(all_upgrades[idx]);
        }

        return result;
    }
}