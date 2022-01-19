using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Dialogue
{    
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue _TestDialogue;
        Dialogue _CurrentDialogue;
        DialogueNode _CurrentNode = null;
        bool _IsPlayerChoosing = false;
        NPC _DialogueNPC;
        string _NPC;

        public event Action onConversantUpdated;

        private void Start()
        {
            StartDialogue(_TestDialogue);
        }

        public void StartDialogue(Dialogue newDIalogue)
        {
            _CurrentDialogue = newDIalogue;
            _CurrentNode = _CurrentDialogue.GetRoodNode();
            _DialogueNPC = _CurrentDialogue.WhoAmITalkingTo();
            TriggerEnterAction();

            if(onConversantUpdated != null) {
                onConversantUpdated();
            }
        }

        public void Quit()
        {
            _CurrentDialogue = null;

            TriggerExitAction();

            _CurrentNode = null;
            _IsPlayerChoosing = false;

            onConversantUpdated();
        }

        public bool IsChoosing()
        {
            if (_CurrentNode.Children.Count > 1)
            {
                _IsPlayerChoosing = true;
            }
            else { _IsPlayerChoosing = false; }

            return _IsPlayerChoosing;
        }

        public string GetText()
        {
            if (_CurrentDialogue == null)
            {
                return "No Dialogue Found";
            }

            return _CurrentNode.Text;
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            _CurrentNode = chosenNode;
            TriggerEnterAction();   
            onConversantUpdated();
        }

        public IEnumerable<DialogueNode> GetChoices() => _CurrentDialogue.GetAllChoices(_CurrentNode);        

        public void Next()
        {
            DialogueNode[] children = _CurrentDialogue.GetAllChildren(_CurrentNode).ToArray();
            _CurrentNode = children[0];
            onConversantUpdated();
        }

        public bool HasNext() => _CurrentDialogue.GetAllChildren(_CurrentNode).Count() == 0;

        public bool IsActive() => _CurrentDialogue != null;

        private void TriggerEnterAction()
        {
            if(_CurrentNode != null && _CurrentNode.OnEnterAction != "")
            {
                Debug.Log(_CurrentNode.OnEnterAction);
            }
        }

        private void TriggerExitAction()
        {
            if (_CurrentNode != null && _CurrentNode.OnExitAction != "")
            {
                Debug.Log(_CurrentNode.OnExitAction);
            }
        }
    }
}
