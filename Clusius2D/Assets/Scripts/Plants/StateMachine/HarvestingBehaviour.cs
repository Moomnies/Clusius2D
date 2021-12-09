using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestingBehaviour : MonoBehaviour, IState
{
    TimerScript timer;

    PlantStateMachine plantReference;

    Seed seed;

    bool harvested;

    PlayerInventory inventory;

    public bool Harvested { get => harvested; set => harvested = value; }

    public HarvestingBehaviour(TimerScript timer, PlantStateMachine plantState)
    {
        this.timer = timer;
        plantReference = plantState;
        inventory = PlayerInventory.GetPlayerInventory();
    }

    public void OnEnter()
    {
        harvested = false;
        seed = plantReference.PlantedSeed;        
    }

    public void Tick()
    {
        Harvested = FarmManager.AddProduceToInventory(seed.TypeOfProduce);        
    }

    public void OnExit()
    {     
        harvested = false;        
        plantReference.ResetPlant();
    }
}
   
