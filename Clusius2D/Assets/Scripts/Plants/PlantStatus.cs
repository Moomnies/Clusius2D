using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantStatus : MonoBehaviour
{
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
        timer.ToggleOnOffTimmer();
        timer.onTimerRunOut += ShowWaterIcon;
    }

    private void Update()
    {
        
    }

    private void ShowWaterIcon()
    {
        if (!statusUI.activeSelf)
        {
            statusUI.SetActive(true);
            iconImage.sprite = needWater;            
        }
    }

    public void FixNeed()
    {     
        if (statusUI.activeSelf)
        {
            statusUI.SetActive(false);
        }
    }
}
