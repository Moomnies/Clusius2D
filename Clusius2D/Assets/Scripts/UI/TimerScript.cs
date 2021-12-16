using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public event Action onTimerRunOut;   
    
    bool isTimerOn = false;
    bool timerIsRunning = false;
    bool isOnHalt;

    float timeCount;
    [SerializeField]
    float currentTimer;

    public float GetCurrentTimer { get => currentTimer; }
    public float GetTimeCount { get => timeCount; }
    public void SetTimerValue(float timeCount) => this.timeCount = timeCount;

    void Update() => CountDownTimer();    
    void ResetTimer() => currentTimer = timeCount;   

    public TimerScript(float timerAmount)
    {
        timeCount = timerAmount;
    }

    void CountDownTimer()
    {
        if (timerIsRunning && currentTimer > -.1 && !isOnHalt)
        {
            currentTimer -= Time.deltaTime;           
        }
        else if (timerIsRunning && currentTimer <= -.1)
        {            
            onTimerRunOut();            
        }
    }   

    public void ToggleOnOffTimmer()
    {
        isTimerOn = !isTimerOn;

        if (isTimerOn)
        {
            timerIsRunning = true;
            currentTimer = timeCount;
            onTimerRunOut += ResetTimer;
            isOnHalt = false;
        }
        else 
        {
            ResetTimer();
            timerIsRunning = false; 
        }
    }   
}
