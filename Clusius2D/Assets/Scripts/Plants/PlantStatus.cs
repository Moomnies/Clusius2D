using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantStatus : MonoBehaviour
{
    [SerializeField]
    float plantQuality;
    [Header("Settings")]
    [SerializeField]
    float timerTillWater;
    [SerializeField]
    PlantStateTD statemachine;    

    [Header("Assign This")]
    [SerializeField]
    GameObject statusUI;
    [SerializeField]
    SpriteRenderer iconImage;    
    [SerializeField]
    TimerScript timer;
    [SerializeField]
    AudioSource audio;

    [Header("Sprites")]
    [SerializeField]
    Sprite needWater;
    [SerializeField]
    Sprite needWeeding;
    [SerializeField]
    Sprite death;

    private void Start()
    {
        timer.SetTimerValue(Random.Range(timerTillWater - 2, timerTillWater + 2));        
    }

    private void Update()
    {
        if (statusUI.activeSelf)
        {
            plantQuality -= Time.deltaTime;
        }
        else { plantQuality += Time.deltaTime; }

        if(plantQuality <= 0)
        {
            PlantIsDead();
        }
    }

    public void PlantIsDead()
    {
        if (!statusUI.activeSelf)
        {
            statusUI.SetActive(true);            
        }   

        iconImage.sprite = death;
        timer.ToggleOnOffTimmer();

        statemachine.ResetPlant();
    }

    public void AllowTimerToRun()
    {
        timer.ToggleOnOffTimmer();
        timer.onTimerRunOut += ShowWaterIcon;
    }

    public void StopWaterTimer()
    {
        timer.ToggleOnOffTimmer();
    }

    private void ShowWaterIcon()
    {
        if (!statusUI.activeSelf)
        {
            statusUI.SetActive(true);
            iconImage.sprite = needWater;
            timer.ToggleOnOffTimmer();
        }
    }

    public void FixNeed()
    {     
        if (statusUI.activeSelf)
        {
            audio.Play();
            statusUI.SetActive(false);
            timer.ToggleOnOffTimmer();
        }
    }
}
