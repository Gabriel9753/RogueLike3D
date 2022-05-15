using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Challanges : MonoBehaviour
{
    public Challange[] challanges;
    public static Challanges instance;

    private bool startedChallange;
    private bool challangeReady;
    public int timeBetweenChallange;
    private int minTime = 40;
    private int maxTime = 150;

    public GameObject Challange_UI;
    public Text challange_text;
    public Text min_text;
    public Text seconds_text;
    public Text amount_text;

    private bool startedTimerForBetweenChallanges;
    private int startedChallanges = 0;

    private Challange activeChallange;

    void Awake(){
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
            return;
        }

        minTime = 5;
        maxTime = 10;
        startedTimerForBetweenChallanges = false;
        startedChallange = false;
        timeBetweenChallange = Random.Range(minTime, maxTime);
        challangeReady = false;
    }

    public void Update(){
        if (WorldManager.instance.startedGame){
            if (!startedTimerForBetweenChallanges && !challangeReady && !startedChallange){
                StartCoroutine(TimerForNextChallange());
            }
            if (challangeReady && !startedChallange && !LevelUpUpgradesUI.Instance.uiActive && !PauseMenu.gameIsPause){
                StartChallange();
            }
        }
    
    }

    private void StartChallange(){
        startedChallanges++;
        challangeReady = false;
        startedChallange = true;
        Challange randomChallange = this.randomChallange();
        activeChallange = randomChallange;

        int timeForChallange = Random.Range(randomChallange.time_seconds, randomChallange.time_seconds + 20);
        int min = timeForChallange / 60;
        float seconds = (timeForChallange / 60 - timeForChallange);
        if (seconds < 0){
            seconds *= -1;
        }


        min_text.text = "" + min + " min";
        seconds_text.text = "" + seconds + " s";
        challange_text.text = randomChallange.text;
        int randomAmount;
        Challange_UI.SetActive(true);
        switch (randomChallange.amount_type){
            case "gold":
                randomAmount = Random.Range(randomChallange.amount - 2, randomChallange.amount + 6) + Player.instance.level * 2;
                randomChallange.updateText(randomAmount);
                challange_text.text = randomChallange.text;
                StartCoroutine(GoldChallange(timeForChallange, randomAmount));
                break;
            case "defeat":
                randomAmount = Random.Range(randomChallange.amount - 2, randomChallange.amount + 2) + Player.instance.level;
                randomChallange.updateText(randomAmount);
                challange_text.text = randomChallange.text;
                StartCoroutine(DefeatChallange(timeForChallange, randomAmount));
                break;
            case "notMove":
                randomAmount = Random.Range(randomChallange.amount - 2, randomChallange.amount + 2) + Player.instance.level/3;
                randomChallange.updateText(randomAmount);
                challange_text.text = randomChallange.text;
                StartCoroutine(NotMoveAndKillChallange(timeForChallange, randomAmount));
                break;
        }
    }

    private Challange randomChallange(){
        //return challanges[Random.Range(0, challanges.Length)];
        return challanges[2];
    }
    
    IEnumerator TimerForNextChallange(){
        startedTimerForBetweenChallanges = true;
        timeBetweenChallange = Random.Range(minTime, maxTime);
        yield return new WaitForSecondsRealtime(timeBetweenChallange);
        startedTimerForBetweenChallanges = false;
        challangeReady = true;
    }

    
    private IEnumerator GoldChallange(int time, int amount){
        int currentGold = (int)Player.instance.gold;
        
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;

            if (Player.instance.gold - currentGold >= amount){
                //Challange completed
                XP_UI.Instance.uncompletedUps++;
                break;
            }

            amount_text.text = "" + ((int) Player.instance.gold - currentGold) + " | " + amount;

            int leftTime = (int)(time - timer);
            int min = leftTime / 60;
            float seconds = (leftTime / 60 - leftTime);
            if (seconds < 0){
                seconds *= -1;
            }

            if (min != 0){
                min_text.text = "" + min + " min";
            }
            else{
                min_text.text = "";
            }
            seconds_text.text = "" + seconds + " s";
            
            yield return null;
        }
        
        //Challange not completed
        Challange_UI.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        
        yield return null;
    }
    
    private IEnumerator DefeatChallange(int time, int amount){
        int currentDefeated = Player.instance.killed_mobs;
        
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;

            if (Player.instance.killed_mobs - currentDefeated >= amount){
                //Challange completed
                XP_UI.Instance.uncompletedUps++;
                break;
            }

            amount_text.text = "" + (Player.instance.killed_mobs - currentDefeated) + " | " + amount;

            int leftTime = (int)(time - timer);
            int min = leftTime / 60;
            float seconds = (leftTime / 60 - leftTime);
            if (seconds < 0){
                seconds *= -1;
            }

            if (min != 0){
                min_text.text = "" + min + " min";
            }
            else{
                min_text.text = "";
            }
            seconds_text.text = "" + seconds + " s";
            
            yield return null;
        }
        
        //Challange not completed
        Challange_UI.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        
        yield return null;
    }
    
    private IEnumerator NotMoveAndKillChallange(int time, int amount){
        int currentDefeated = Player.instance.killed_mobs;
        Vector3 currentPosition = Player.instance.transform.position;

        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;
            
            if (timer > 4 && Player.instance.isRunning()){
                break;
            }

            if (Player.instance.killed_mobs - currentDefeated >= amount){
                //Challange completed
                XP_UI.Instance.uncompletedUps++;
                break;
            }

            amount_text.text = "" + (Player.instance.killed_mobs - currentDefeated) + " | " + amount;

            int leftTime = (int)(time - timer);
            int min = leftTime / 60;
            float seconds = (leftTime / 60 - leftTime);
            if (seconds < 0){
                seconds *= -1;
            }

            if (min != 0){
                min_text.text = "" + min + " min";
            }
            else{
                min_text.text = "";
            }
            seconds_text.text = "" + seconds + " s";
            
            yield return null;
        }
        
        //Challange not completed
        Challange_UI.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        
        yield return null;
    }
    
}
