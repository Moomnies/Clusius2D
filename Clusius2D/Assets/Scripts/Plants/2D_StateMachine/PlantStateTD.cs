using System;
using UnityEngine;

public class PlantStateTD : MonoBehaviour
{
    StateMachine stateMachine;
    Seed plantedSeed = null;

    string plantId;
    bool plantNeedsCare;

    [Header("Assign This")]
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Collider2D objectCollider;
    [SerializeField]
    TimerScript timer;
    [SerializeField]
    PlantStatus plantStatus;

    [Header("Debug")]
    [SerializeField]
    bool debugMode;

    public string GetPlantId { get => plantId; }
    public Seed PlantedSeed { get => plantedSeed; set => plantedSeed = value; }
    public TimerScript Timer { get => timer; set => timer = value; }

    event Action plantIsPlanted;

    private void Awake()
    {
        stateMachine = new StateMachine(debugMode);

        var idleTD = new IdleTD(this);
        var growingTD = new GrowingTD(this, timer);
        var harvestTD = new HarvestTD(this);
        var regrowthTD = new RegrowingTD(this, timer);

        void At(IState to, IState from, Func<bool> condition)
        {
            stateMachine.AddTransition(to, from, condition);
        }

        void Hook(Func<bool> condition, Action action, Action method)
        {
            stateMachine.AddTransitionAction(condition, action, method);
        }

        At(idleTD, growingTD, seedIsPlanted());
        At(growingTD, harvestTD, plantIsDoneGrowing());
        At(harvestTD, regrowthTD, waitForRegrowth());
        At(harvestTD, idleTD, plantIsHarvested());
        At(regrowthTD, harvestTD, plantIsDoneGrowing());

        Func<bool> seedIsPlanted() => () => plantedSeed != null;
        Func<bool> waitForRegrowth() => () => plantedSeed.RegrowProduce && harvestTD.Harvested;
        Func<bool> plantIsDoneGrowing() => () => growingTD.PlantIsDoneGrowing;
        Func<bool> plantIsHarvested() => () => harvestTD.Harvested;

        Hook(seedIsPlanted(), plantIsPlanted, plantStatus.AllowTimerToRun);
        
        stateMachine.SetState(idleTD);
    }

    private void Start()
    {
        plantId = Guid.NewGuid().ToString();
        FarmManager.AddMeToManager(this);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

            if (objectCollider == touchedCollider)
            {
                if (touch.phase == TouchPhase.Began)
                {                   
                    FarmManager.ThisPlantIsTouched(plantId);
                }
            }
        }
    }
    public void SetPlantSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void ExecuteBehaviourOnClick()
    {
        stateMachine.Tick();
    }

    public void ResetPlant()
    {
        plantedSeed = null;
        spriteRenderer.sprite = null;
    }
}
