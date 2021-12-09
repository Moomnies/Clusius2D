using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour, IState
{
    SpriteRenderer dirtSprite;

    bool holeIsDug = false;

    public bool IsHoleDug { get => holeIsDug; }

    public Digging(SpriteRenderer spriteRenderer)
    {
        dirtSprite = spriteRenderer;
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
