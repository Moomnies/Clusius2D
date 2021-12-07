using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    [SerializeField]
    string _NPCName;

    [SerializeField]
    string _ID;

    public string Name { get => _NPCName; }

    private void Awake() => _ID = Guid.NewGuid().ToString();
}
