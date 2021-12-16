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
        timer.SetTimerValue(timerTillWater);
        timer.ToggleOnOffTimmer();
        timer.onTimerRunOut += ShowWaterIcon;
    }

    private void ShowWaterIcon()
    {
        if (!statusUI.activeSelf)
        {
            statusUI.SetActive(true);
            iconImage.sprite = needWater;
        }
    }
}
