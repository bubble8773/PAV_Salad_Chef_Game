using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float waitingTime = 0;
    public bool timerIsRunning = false;
    public Slider slider;
    
    private void Start()
    {

    }

    void Update()
    {

        if (timerIsRunning)
        {
            if (waitingTime > 0)
            {
                //calculate time
                waitingTime -= Time.deltaTime;

                DisplayTime(waitingTime);


            }
            else
            {
                //reset time
                waitingTime = 0;
                timerIsRunning = false;

            }
        }
       
    }

    void DisplayTime(float timeToDisplay) {
        slider.value = timeToDisplay;
    }
}