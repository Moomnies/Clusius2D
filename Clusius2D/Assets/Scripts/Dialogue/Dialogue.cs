using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace RPG.Dialogue
{
    /* TODO: NPC Speaker
        >> Turn NPC into List, Make IEnumirator Property
        >> Because Dictionaries arent serializable use method to check OnValidate?
        >> Make Variable for NPC storage per Node > DONE
        >> Add Feature to editor to select NPC 
        >> Make Sure Dialogue Manager gets the right NPC Name
    */


    //Made Class serializable because this apparantly helps with the Undo.RecordObject
    [Serializable]
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        //Variables
        [SerializeField]
        List<DialogueNode> _Nodes = new List<DialogueNode>();

        [SerializeField]
        Dictionary<string, DialogueNode> _NodeLookup = new Dictionary<string, DialogueNode>();

        [SerializeField]
        Vector2 _NewNodeOffset = new Vector2(250, 0);

        [SerializeField]
        NPC _TalkingCharacter;

        [SerializeField]
        List<NPC> NPCInConversation = new List<NPC>();

        //Properties
        public IEnumerable<DialogueNode> GetAllNodes { get => _Nodes; }

        public NPC WhoAmITalkingTo() => _TalkingCharacter;

        /// <summary>
        /// Gets Rootnode from Node list
        /// </summary>
        /// <returns>Returns RootNode from Node List</returns>
        public DialogueNode GetRoodNode() => _Nodes[0];

#if UNITY_EDITOR
        //If _Nodes has no Nodes adds new Node [EDITOR ONLY]
        private void Awake() => OnValidate();     
#endif
        /// <summary>
        /// Is called when value is changed in inspector or when SO is loaded
        /// </summary>
        private void OnValidate()
        {
            _NodeLookup.Clear();           

            foreach (DialogueNode node in GetAllNodes)
            {
                _NodeLookup[node.name] = node;
            }   
            
            if(NPCInConversation.Count() != NPCInConversation.Distinct().Count())
            {
                Debug.LogError("Duplicate NPC in Dialogue: " + this.name);
            }
        }
     

        /// <summary>
        /// Returns all Children from ParentNode
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns> All Children from Parent Node </returns>
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.Children)
            {
                if (_NodeLookup.ContainsKey(childID))
                {
                    yield return _NodeLookup[childID];
                }
            }
        }
#if UNITY_EDITOR

        /// <summary>
        /// Create Node without parent is called when no nodes are available in Dialogue
        /// </summary>
        /// <returns></returns>
        public DialogueNode CreateNode()
        {
            DialogueNode newNode = CreateInstance<NPCNode>();   

            return newNode;
        }

        /// <summary>
        /// Creates Node and adds this to Child class
        /// </summary>
        /// <param name="parent"> Parent node of Node that will be created </param>
        /// <param name="nodeType"> String specifying the type of node that has to be created (NPC or Player) </param>
        public void CreateNode(DialogueNode parent, string nodeType)
        {
            DialogueNode newNode;

            if (nodeType == "NPC")
            {
                newNode = setNewNodeData(parent, MakeNPCNode(parent));
                //setSpeaker(newNode as NPCNode, parent);
            }
            else { newNode = setNewNodeData(parent, MakePlayerNode(parent));  }            

            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Undo Creating Node");

            AddNode(newNode);
        }

        private DialogueNode setNewNodeData(DialogueNode parent, DialogueNode newNode)
        {
            newNode.name = Guid.NewGuid().ToString();

            parent.AddChild(newNode.name);
            newNode.SetPosition(parent.RectPosition + _NewNodeOffset);
            newNode.Parent = parent.name;

            return newNode;
        }

        private void setSpeaker(NPCNode aNewNPCNode, DialogueNode parent)
        {
            DialogueNode dialogueNode = parent;
            String parentID = parent.Parent;

            if (dialogueNode.GetType() == typeof(NPCNode))
            {
                NPCNode aNPCNode = dialogueNode as NPCNode;
                aNewNPCNode.Speaker = aNPCNode.Speaker;
            }

            while (dialogueNode.GetType() != typeof(NPCNode))
            {
                parentID = dialogueNode.Parent;
                dialogueNode = _NodeLookup[parentID];                
            }

            NPCNode anotherNPCNode = dialogueNode as NPCNode;
            aNewNPCNode.Speaker = anotherNPCNode.Speaker;
        }

        /// <summary>
        /// Creates new Player Node
        /// </summary>
        /// <param name="parent"> Parent node of Node that will be created </param>
        /// <returns> Created Node </returns>
        private DialogueNode MakePlayerNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<PlayerNode>();

            return newNode;
        }

        /// <summary>
        /// Creates new NPC node and assigns Speaker 
        /// </summary>
        /// <param name="parent">Parent node of Node that will be created</param>
        /// <returns> Created Node </returns>
        private DialogueNode MakeNPCNode(DialogueNode parent)
        {
            NPCNode newNode = CreateInstance<NPCNode>();        

            return newNode;
        }

        public IEnumerable<DialogueNode> GetAllChoices(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                 yield return node;                
            }
        }

        /// <summary>
        /// Deletes Node from SO
        /// </summary>
        /// <param name="nodeToDelete">Node that has to be deleted</param>
        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Undo Deleting Node");
            _Nodes.Remove(nodeToDelete);   
            
            OnValidate();            

            CleanChildrenDeletedNode(nodeToDelete);

            Undo.DestroyObjectImmediate(nodeToDelete);

        }

        /// <summary>
        /// Adds node to Node List
        /// </summary>
        /// <param name="newNode"> Node that needs to be added </param>
        private void AddNode(DialogueNode newNode)
        {
            _Nodes.Add(newNode);
            OnValidate();
        }    

        /// <summary>
        /// Deletes all child nodes that are attached to the parent node in Children Dictionary
        /// </summary>
        /// <param name="nodeToDelete">Node that has been deleted</param>
        private void CleanChildrenDeletedNode(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes)
            {
                node.RemoveChild(nodeToDelete.name);               
            }
        }
#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR

            if (_Nodes.Count == 0)
            {
                DialogueNode newNode = CreateNode();             
                AddNode(newNode);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
               foreach (DialogueNode node in GetAllNodes)
               {
                   if(AssetDatabase.GetAssetPath(node) == "")
                   {
                       AssetDatabase.AddObjectToAsset(node, this);
                   }
               }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}
