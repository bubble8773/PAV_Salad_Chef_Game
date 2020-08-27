using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float waitingTime = 0;
    public float initialTime;
    public bool timerIsRunning = false;
    public Slider slider;
    public string message;

    public static Timer _instance;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        if (slider != null)
            slider.maxValue = waitingTime;

        if (gameObject.name.Contains("Chopping"))
            return;
        
            timerIsRunning = true;
          
    }

    void Update()
    {

        if (timerIsRunning)
        {
            if (waitingTime > 0)
            {
                //calculate time
                waitingTime -= Time.deltaTime;
                
            }
            else
            {
                //reset time
                waitingTime = 0;
                timerIsRunning = false;
                if (gameObject.name.Contains("Customer"))
                {
                    
                    GameManager._instance.UpdateScores(-2, GameManager._instance.player1);
                    GameManager._instance.UpdateScores(-2, GameManager._instance.player2);
                    gameObject.GetComponent<Orders>().saladCombo.Clear();
                    RestartTimer(2.0f);
                }

                if (gameObject.name == "GameManager")
                {
                    Debug.Log("gameover");
                    GameManager._instance.isGameOver = true;
                }

                if (gameObject.name.Contains("Chopping"))
                {
                    message = "Done Chopping";

                }
            }
            
        }
        if (slider != null)
            DisplayTime(waitingTime);
    }

    void DisplayTime(float timeToDisplay) {
        
        slider.value = timeToDisplay;
    }

    public void RestartTimer(float amount)
    {
        StartCoroutine(AddTimeDelay(amount));
    }
    IEnumerator AddTimeDelay(float amount)
    {
        Debug.Log("restart timer");
        yield return new WaitForSeconds(amount);
        waitingTime = initialTime;
        timerIsRunning = true;
    
    }
}