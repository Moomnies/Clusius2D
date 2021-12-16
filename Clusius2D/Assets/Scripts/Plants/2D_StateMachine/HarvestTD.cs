using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTD : MonoBehaviour, IState
{
    PlayerInventory inventory;
    PlantStateTD stateMachine;

    Seed plantedSeed;

    bool harvested;
    public bool Harvested { get => harvested; set => harvested = value; }
    public HarvestTD(PlantStateTD plantStateMachine)
    {
        stateMachine = plantStateMachine;
        
    }   

    public void OnEnter()
    {
        harvested = false;
        plantedSeed = stateMachine.PlantedSeed;

        stateMachine.SetPlantSprite(plantedSeed.PlantSprites[plantedSeed.PlantSprites.Length - 1]);
    }

    public void OnExit()
    {
        if (!stateMachine.PlantedSeed.RegrowProduce)
        {
            stateMachine.ResetPlant();
        };
    }

    public void Tick()
    {
        harvested = FarmManager.AddProduceToInventory(plantedSeed.TypeOfProduce);        
    }
}
