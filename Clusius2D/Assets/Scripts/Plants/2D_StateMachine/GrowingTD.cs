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

    public GrowingTD(PlantStateTD plantStateMachine)
    {
        stateMachine = plantStateMachine;
    }

    public void OnEnter()
    {
        plantedSeed = stateMachine.PlantedSeed;

        if(plantTimer != null)
        {
            plantTimer.SetTimerValue(plantedSeed.TimeToGrow);
            plantTimer.ToggleOnOffTimmer();               
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
