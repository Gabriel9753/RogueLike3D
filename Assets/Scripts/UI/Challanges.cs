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
    private int completedChallanges = 0;

    private Challange activeChallange;

    public GameObject success;
    public GameObject failed;
    private GameObject challangeImage;

    void Awake(){
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        challangeImage = Challange_UI.transform.GetChild(0).gameObject;
        Challange_UI.SetActive(true);
        challangeImage.SetActive(false);
        print(challangeImage.name);
        minTime = 5;
        maxTime = 10;
        startedTimerForBetweenChallanges = false;
        startedChallange = false;
        timeBetweenChallange = Random.Range(minTime, maxTime);
        challangeReady = false;
        startedChallanges = 0;
        completedChallanges = 0;
    }

    public void Update(){
        if (WorldManager.instance.startedGame){
            if (!startedTimerForBetweenChallanges && !challangeReady && !startedChallange){
                StartCoroutine(TimerForNextChallange());
            }
            if (completedChallanges < 30 && challangeReady && !startedChallange && !LevelUpUpgradesUI.Instance.uiActive && !PauseMenu.gameIsPause){
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
        seconds_text.text = "" + seconds%60 + " s";
        challange_text.text = randomChallange.text;
        int randomAmount;
        challangeImage.SetActive(true);
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
            case "noHP":
                timeForChallange = Random.Range(randomChallange.time_seconds, randomChallange.time_seconds + 15);
                randomChallange.updateText();
                challange_text.text = randomChallange.text;
                amount_text.text = "";
                StartCoroutine(noDamageChallange(timeForChallange, -1));
                break;
            case "noAbilities":
                timeForChallange = Random.Range(randomChallange.time_seconds, randomChallange.time_seconds + 15);
                randomChallange.updateText();
                challange_text.text = randomChallange.text;
                amount_text.text = "";
                StartCoroutine(noAbilitiesChallange(timeForChallange, -1));
                break;
            case "traveller":
                timeForChallange = Random.Range(randomChallange.time_seconds, randomChallange.time_seconds + 15);
                randomChallange.updateText();
                challange_text.text = randomChallange.text;
                amount_text.text = "";
                StartCoroutine(travellerChallange(timeForChallange, -1));
                break;
        }
    }

    private Challange randomChallange(){
        return challanges[Random.Range(0, challanges.Length)];
    }
    
    IEnumerator TimerForNextChallange(){
        startedTimerForBetweenChallanges = true;
        timeBetweenChallange = Random.Range(minTime, maxTime);
        float timer = 0.0f;
        while(timer < timeBetweenChallange){
            timer += Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSecondsRealtime(timeBetweenChallange);
        startedTimerForBetweenChallanges = false;
        challangeReady = true;
        yield return null;
    }

    
    private IEnumerator GoldChallange(int time, int amount){
        int currentGold = (int)Player.instance.gold;
        bool failedCh = true;
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;

            if (Player.instance.gold - currentGold >= amount){
                //Challange completed
                XP_UI.Instance.uncompletedUps++;
                completedChallanges++;
                failedCh = false;
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
            seconds_text.text = "" + seconds%60 + " s";
            
            yield return null;
        }
        
        //Challange not completed
        challangeImage.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        StartCoroutine(TimerFeedback(failedCh));
        yield return null;
    }
    
    private IEnumerator DefeatChallange(int time, int amount){
        int currentDefeated = Player.instance.killed_mobs;
        bool failedCh = true;
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;

            if (Player.instance.killed_mobs - currentDefeated >= amount){
                //Challange completed
                XP_UI.Instance.uncompletedUps++;
                completedChallanges++;
                failedCh = false;
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
            seconds_text.text = "" + seconds%60 + " s";
            
            yield return null;
        }
        
        //Challange not completed
        challangeImage.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        StartCoroutine(TimerFeedback(failedCh));
        yield return null;
    }
    
    private IEnumerator NotMoveAndKillChallange(int time, int amount){
        bool failedCh = true;
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
                completedChallanges++;
                failedCh = false;
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
            seconds_text.text = "" + seconds%60 + " s";
            
            yield return null;
        }
        
        //Challange not completed
        challangeImage.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        StartCoroutine(TimerFeedback(failedCh));
        yield return null;
    }

    private IEnumerator noDamageChallange(int time, int amount){
        float tempRegen = Player.instance.healthRegen;
        Player.instance.healthRegen = 0;
        int currentHealth = (int)Player.instance.health;
        bool failedCh = false;
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;
            
            if (Player.instance.healthRegen > 0){
                tempRegen += Player.instance.healthRegen;
                Player.instance.healthRegen = 0;
            }
            
            if (Player.instance.health < currentHealth){
                failedCh = true;
                break;
            }
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
            seconds_text.text = "" + seconds%60 + " s";
            
            yield return null;
        }

        if (!failedCh){
            //Challange completed
            XP_UI.Instance.uncompletedUps++;
            completedChallanges++;
        }
        //Challange not completed
        challangeImage.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        Player.instance.healthRegen = tempRegen;
        StartCoroutine(TimerFeedback(failedCh));
        yield return null;
    }
    private IEnumerator noAbilitiesChallange(int time, int amount){
        float tempRegen = Player.instance.manaRegen;
        Player.instance.manaRegen = 0;
        int currentMana = (int)Player.instance.mana;
        bool failedCh = false;
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;

            if (Player.instance.manaRegen > 0){
                tempRegen += Player.instance.manaRegen;
                Player.instance.manaRegen = 0;
            }
            
            if (Player.instance.mana < currentMana){
                failedCh = true;
                break;
            }
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
            seconds_text.text = "" + seconds%60 + " s";
            
            yield return null;
        }

        if (!failedCh){
            //Challange completed
            XP_UI.Instance.uncompletedUps++;
            completedChallanges++;
        }
        
        //Challange not completed
        
        startedChallange = false;
        challangeReady = false;
        challangeImage.SetActive(false);
        Player.instance.manaRegen = tempRegen;
        StartCoroutine(TimerFeedback(failedCh));
        yield return null;
    }
   
    private IEnumerator travellerChallange(int time, int amount){
        int currentMana = (int)Player.instance.mana;
        bool failedCh = false;
        float timer = 0.0f;
        while(timer < time){
            timer += Time.deltaTime;

            if (timer > 4 && !Player.instance.isRunning()){
                failedCh = true;
                break;
            }
            
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
            seconds_text.text = "" + seconds%60 + " s";
            
            yield return null;
        }

        if (!failedCh){
            //Challange completed
            XP_UI.Instance.uncompletedUps++;
            completedChallanges++;
        }
        
        //Challange not completed
        challangeImage.SetActive(false);
        startedChallange = false;
        challangeReady = false;
        StartCoroutine(TimerFeedback(failedCh));
        yield return null;
    }
    
    private IEnumerator TimerFeedback(bool failedChallange){
        if (failedChallange){
            float timer = 2f;
            float time = 0;
            failed.SetActive(true);
            while (time < timer){
                time += Time.deltaTime;
                yield return null;
            }
            failed.SetActive(false);
        }
        else{
            float timer = 2f;
            float time = 0;
            success.SetActive(true);
            while (time < timer){
                time += Time.deltaTime;
                yield return null;
            }
            success.SetActive(false);
        }
    }
}
