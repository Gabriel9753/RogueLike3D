using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : MonoBehaviour
{    
    //Manage stuff:
    public bool startedGame = false;
    public Config config;

    public float timer;
    public bool stopTime = false;

    #region Singleton
    public static WorldManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    

    public void Update(){
        //TODO:TIMER
        if (stopTime){
            timer += Time.deltaTime;
        }
    }
    public void StartRun(){
        Player.instance.GetComponent<AbilityHolder>().enabled = true;
        Player.instance.resetStats();
        Skills_menu_in_game.Instance.Skills_HUD_element.SetActive(true);
        stopTime = true;
        instance.startedGame = true;
        AudioManager.instance.Stop("Base_soundtrack");
    }

}