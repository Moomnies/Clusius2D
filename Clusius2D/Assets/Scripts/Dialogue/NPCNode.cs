using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class NPCNode : DialogueNode
    {
        [SerializeField]
        NPC _Speaker;

        public NPC Speaker{get => _Speaker; set => _Speaker = value;}
    }
}
