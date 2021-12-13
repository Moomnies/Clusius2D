using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Seed", menuName = "InventoryItem/Seed")]
    public class Seed : Item
    {
        [Header("Seed Data")]
        [SerializeField]  
        float _TimeToGrow;
        [SerializeField]
        Sprite[] plantSprites = new Sprite[0];

        [Header("Produce Data")]
        [SerializeField]
        Produce _Produce;
        [SerializeField]
        float _MaxNumberOfProduce;
        [SerializeField]
        bool regrowProduce;

        //Getter and Setter for Acces Variables
        public float TimeToGrow { get => _TimeToGrow; }
        public Sprite[] PlantSprites { get => plantSprites; }
        public Produce TypeOfProduce { get => _Produce; }
        public float MaxNumberOfProduce { get => _MaxNumberOfProduce; }
        public bool RegrowProduce { get => regrowProduce; }    
}



