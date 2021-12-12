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
    Collider2D objectCollider;
    [SerializeField]
    TimerScript timer;


    [SerializeField]
    Color availabilityColor;

    [Header("Debug")]
    [SerializeField]
    bool debugMode;
    [SerializeField]
    Seed plantedSeed = null;
 

    public string GetPlantId { get => plantId;}
    public Seed PlantedSeed { get => plantedSeed; set => plantedSeed = value; }
    public TimerScript Timer { get => timer; set => timer = value; }
    private void Awake()
    {
        stateMachine = new StateMachine(debugMode);

        var idleTD = new IdleTD(this);
        var growingTD = new GrowingTD(this, timer);

        void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);

        At(idleTD, growingTD, seedIsPlanted());

        Func<bool> seedIsPlanted() => () => plantedSeed != null;

        stateMachine.SetState(idleTD);
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

            if(objectCollider == touchedCollider)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    FarmManager.ThisPlantIsTouched(plantId);
                }                
            }
        }
    }

    private void Start()
    {
        plantId = Guid.NewGuid().ToString();
        FarmManager.AddMeToManager(this);
    }

    public void SetPlantSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void ExecuteBehaviourOnClick() => stateMachine.Tick();

    public void resetPlant()
    {
        plantedSeed = null;
    }
   
}
