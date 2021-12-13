using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingTD : MonoBehaviour, IState
{
    PlantStateTD stateMachine;    
    TimerScript plantTimer;

    Seed plantedSeed;
    Sprite[] plantStageSprites;

    int orderInPlantStage;

    public int OrderInPlantStage { get => orderInPlantStage; set => orderInPlantStage = value; }

    public GrowingTD(PlantStateTD plantStateMachine, TimerScript timer)
    {
        stateMachine = plantStateMachine;
        plantTimer = timer;
    }

    public void OnEnter()
    {
        plantedSeed = stateMachine.PlantedSeed;
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
            Debug.Log(orderInPlantStage);


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
        if (orderInPlantStage == plantStageSprites.Length - 1)
        {
            stateMachine.ExecuteBehaviourOnClick();
            return;
        }

        orderInPlantStage++;
        stateMachine.SetPlantSprite(plantStageSprites[orderInPlantStage]);
    }
}
