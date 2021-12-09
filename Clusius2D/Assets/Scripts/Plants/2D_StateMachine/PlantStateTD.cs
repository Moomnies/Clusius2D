using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStateTD : MonoBehaviour
{
    StateMachine stateMachine;
    string plantId;

    [Header("Assign This")]
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite startSprite;
    
    [SerializeField]
    Color availabilityColor;

    [Header("Debug")]
    [SerializeField]
    bool debugMode;
    [SerializeField]
    Seed plantedSeed = null;

    public string GetPlantId { get => plantId;}
    public Seed PlantedSeed { get => plantedSeed; set => plantedSeed = value; }

    private void Awake()
    {
        stateMachine = new StateMachine(debugMode);

        var idleTD = new IdleTD(this);        

        void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);

        stateMachine.SetState(idleTD);
    }

    private void Start()
    {
        plantId = Guid.NewGuid().ToString();
        //FarmManager.AddMeToManager(this);
    }

    public void SetPlantSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void ExecuteBehaviourOnClick() => stateMachine.Tick();
   
}
