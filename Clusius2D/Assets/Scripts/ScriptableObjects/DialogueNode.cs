using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace RPG.Dialogue
{
    
    public class DialogueNode : ScriptableObject
    {            
        [SerializeField]
        string _Text;
        [SerializeField]
        List<string> _Children = new List<string>();
        [SerializeField]
        Rect _Rect = new Rect(0, 0, 200, 100);
        [SerializeField]
        String _Parent;
     
        string onEnterAction;       
        string onExitAction;

        public string Text { get => _Text; }
        public List<string> Children { get => _Children; set => _Children = value; }
        public Rect Rect { get => _Rect; set => _Rect = value; }
        public Vector2 RectPosition { get => _Rect.position; }
        public string OnEnterAction { get => onEnterAction; set => onEnterAction = value; }
        public string OnExitAction { get => onExitAction; set => onExitAction = value; }
        public string Parent { get => _Parent; set => _Parent = value; }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Undo Movement");
            _Rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText != _Text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                _Text = newText;
                EditorUtility.SetDirty(this);
            }
        }    

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Dialogue Link");
            _Children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            _Children.Remove(childID);
            EditorUtility.SetDirty(this);
        }
#endif
    }
}

