using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : ScriptableObject
{
    [SerializeField] [Tooltip("Time it will take for plant to go to next stage. I calculated in seconds so 300 is 5 minutes.")] 
    float _TimeToGrow;
    [SerializeField] [Tooltip("Currently takes in Meshes for the 4 stages of the plant. Will be changed to Models in the future.")] 
    Sprite[] _PlantModels = new Sprite[4];
    [SerializeField] [Tooltip("SO of Item that needs to be added to Inventory on Harvest.")] 
    Item _Produce;
    [SerializeField] [Tooltip("Maximum number of produce that can be taken from this plant.")]
    float _MaxNumberOfProduce;

    //Getter and Setter for Acces Variables
    public float TimeToGrow { get => _TimeToGrow;}
    public Sprite[] PlantModels { get => _PlantModels;}
    public Item TypeOfProduce { get => _Produce; }  
    public float MaxNumberOfProduce { get => _MaxNumberOfProduce; }
}