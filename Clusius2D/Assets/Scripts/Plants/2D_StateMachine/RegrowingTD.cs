using UnityEngine;

public class RegrowingTD : MonoBehaviour, IState
{
    PlantStateTD stateMachine;

    TimerScript plantTimer;

    bool plantIsDoneGrowing;

    Seed plantedSeed;
    public bool PlantIsDoneGrowing { get => plantIsDoneGrowing; }

    public RegrowingTD(PlantStateTD plantStateMachine, TimerScript timer)
    {
        stateMachine = plantStateMachine;
        plantTimer = timer;
    }

    public void OnEnter()
    {
        plantedSeed = stateMachine.PlantedSeed;

        if (plantTimer != null)
        {
            plantTimer.SetTimerValue(plantedSeed.TimeToGrow);
            plantTimer.ToggleOnOffTimmer();
            plantTimer.onTimerRunOut += NextPlantStage;
        }
        else { Debug.LogFormat("GROWING.TICK(): Timer is null! Timer: {0} In Plant: {1}", plantTimer, stateMachine.GetPlantId); }

        stateMachine.SetPlantSprite(plantedSeed.PlantSprites[plantedSeed.PlantSprites.Length - 2]);
    }

    public void OnExit()
    {
        plantTimer.onTimerRunOut -= NextPlantStage;
        plantTimer.ToggleOnOffTimmer();
        plantIsDoneGrowing = false;
    }

    public void Tick()
    {

    }

    public void NextPlantStage()
    {
        plantIsDoneGrowing = true;
        stateMachine.ExecuteBehaviourOnClick();
    }
}
