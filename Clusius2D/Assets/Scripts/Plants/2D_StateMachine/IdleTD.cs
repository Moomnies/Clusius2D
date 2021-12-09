using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTD : MonoBehaviour, IState
{
    PlantStateTD stateMachine;

    public IdleTD(PlantStateTD plantStateMachine)
    {
        stateMachine = plantStateMachine;
    }

    public void OnEnter()
    {
        if (stateMachine.GetPlantId != null)
        {
            FarmManager.PlayerNeedsToSelectPlant(stateMachine.GetPlantId);
        }
    }

    public void OnExit()
    {
       
    }

    public void Tick()
    {
       
    }
}
