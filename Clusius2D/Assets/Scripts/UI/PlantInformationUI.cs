using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInformationUI : MonoBehaviour
{
    [SerializeField] TMP_Text timer;
    [SerializeField] TMP_Text plantName;

    [SerializeField] Slider progessionSlider;
    [SerializeField] AudioSource popSound;

    TimerScript plantTimer;

    public void SetTimerScript(TimerScript timer)
    {
        plantTimer = timer;
    }

    public void SetPlantName(string plantName)
    {
        this.plantName.text = plantName;
        popSound.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        FarmManager.AttachUIComponent(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (plantTimer != null && progessionSlider != null)
        {
            DisplayTime(plantTimer.GetCurrentTimer);
            progessionSlider.value = 1 - (plantTimer.GetCurrentTimer / plantTimer.GetTimeCount);            
        }       
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}  
