using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTD : MonoBehaviour, IState
{
    PlayerInventory inventory;
    PlantStateTD plantStateMachine;

    Seed plantedSeed;

    bool harvested;
    public bool Harvested { get => harvested; set => harvested = value; }
    public HarvestTD(PlantStateTD stateMachine)
    {
        plantStateMachine = stateMachine;
        
    }   

    public void OnEnter()
    {
        harvested = false;
        plantedSeed = plantStateMachine.PlantedSeed;
    }

    public void OnExit()
    {
        plantStateMachine.ResetPlant();
    }

    public void Tick()
    {
        harvested = FarmManager.AddProduceToInventory(plantedSeed.TypeOfProduce);        
    }
}
