using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    private static int enemiesCreated = 0;
    public int enemyID;
    // Start is called before the first frame update
    void Start(){
        enemyID = enemiesCreated;
        enemiesCreated++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
