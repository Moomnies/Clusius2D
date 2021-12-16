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

    public void OnContinue()
    {
        throw new System.NotImplementedException();
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
       
    }

    public void OnPauze()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        if (stateMachine.GetPlantId != null)
        {
            FarmManager.PlayerNeedsToSelectPlant(stateMachine.GetPlantId);
        }
    }
}
