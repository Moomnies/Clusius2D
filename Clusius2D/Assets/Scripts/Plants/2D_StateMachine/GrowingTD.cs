using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingTD : MonoBehaviour, IState
{
    PlantStateTD stateMachine;    
    TimerScript plantTimer;

    Seed plantedSeed;
    Sprite[] plantStageSprites;
    bool plantIsDoneGrowing;

    int orderInPlantStage;

    public int OrderInPlantStage { get => orderInPlantStage; set => orderInPlantStage = value; }
    public bool PlantIsDoneGrowing { get => plantIsDoneGrowing; }

    public GrowingTD(PlantStateTD plantStateMachine, TimerScript timer)
    {
        stateMachine = plantStateMachine;
        plantTimer = timer;
    }

    public void OnEnter()
    {
        plantedSeed = stateMachine.PlantedSeed;
        plantIsDoneGrowing = false;
        orderInPlantStage = 0;

        if(plantTimer != null)
        {
            plantTimer.SetTimerValue(plantedSeed.TimeToGrow);
            plantTimer.ToggleOnOffTimmer();
            plantTimer.onTimerRunOut += NextPlantStage;          
        }
        else { Debug.LogFormat("GROWING.TICK(): Timer is null! Timer: {0} In Plant: {1}", plantTimer, stateMachine.GetPlantId); }

      
        if (plantedSeed.PlantSprites != null)
        {
            plantStageSprites = new Sprite[plantedSeed.PlantSprites.Length];          

            for (int i = 0; i < plantStageSprites.Length; i++)
            {
                plantStageSprites[i] = plantedSeed.PlantSprites[i];
            }

            stateMachine.SetPlantSprite(plantStageSprites[orderInPlantStage]);
        }
    }

    public void Tick()
    {
       
    }

    public void OnExit()
    {
        plantTimer.onTimerRunOut -= NextPlantStage;
        plantTimer.ToggleOnOffTimmer();
    }   

    public void NextPlantStage()
    {
        if (orderInPlantStage == plantStageSprites.Length - 2)
        {
            plantIsDoneGrowing = true;
            stateMachine.ExecuteBehaviourOnClick();
            return;
        }

        orderInPlantStage++;
        stateMachine.SetPlantSprite(plantStageSprites[orderInPlantStage]);
    }
}
