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

        harvested = false;
    }

    public void Tick()
    {
        harvested = FarmManager.AddProduceToInventory(plantedSeed.TypeOfProduce);

        if (harvested)
        {
            stateMachine.ExecuteBehaviourOnClick();
        }
    }

    public void OnPauze()
    {
        throw new System.NotImplementedException();
    }

    public void OnContinue()
    {
        throw new System.NotImplementedException();
    }
}
