using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour, IState
{
    Sprite dirtSprite;

    bool holeIsDug = false;

    public bool IsHoleDug { get => holeIsDug; }

    public Digging(Sprite meshFilter)
    {
        //Set Sprite to Sprite in Constructor
    }

    public void OnEnter()
    {
        holeIsDug = true;
    }
    public void Tick()
    {
        
    }

    public void OnExit()
    {
        holeIsDug = false; 
    }    
}
