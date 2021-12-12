using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : ScriptableObject
{
    [SerializeField] 
    float timeToGrow;
    [SerializeField]
    Sprite[] plantModels = new Sprite[0];
    [SerializeField]
    Sprite spriteAfterHarvest;
    [SerializeField]
    Item produce;
    [SerializeField]
    float maxNumberOfProduce;

    //Getter and Setter for Acces Variables
    public float TimeToGrow { get => timeToGrow;}
    public Sprite[] PlantModels { get => plantModels;}
    public Item TypeOfProduce { get => produce; }  
    public float MaxNumberOfProduce { get => maxNumberOfProduce; }
}