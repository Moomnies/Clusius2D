using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace RPG.Dialogue.Editor
{     
    public class DialogueEditor : EditorWindow
    {
        //SO Dialogue that is selected, will be null is there is no Dialogue opent in Window
        Dialogue _SelectedDialogue = null;

        [NonSerialized]
        GUIStyle _NodeStyle;
        [NonSerialized]
        GUIStyle _PlayerNodeStyle;
       
        //Used to store Node player is dragging in Dialogue Editor     
        DialogueNode _DraggingNode = null;

        //Used to create new node
        [NonSerialized]
        DialogueNode _CreatingNode = null;

        [NonSerialized]
        String _TypeToMake = null;

        //Used to store node to Delete
        [NonSerialized]
        DialogueNode _DeletingNode = null;

        //Used to store Parent Node to link with Child
        [NonSerialized]
        DialogueNode _LinkingParent = null;

        //Used to calculate offset so nodes don't snap to Mouse Curser with left corner
        Vector2 _DraggingOffset;

        //Used for Scroll Window 
        Vector2 _ScrollPosition;

        [NonSerialized]
        bool _DraggingCanvas = false;

        [NonSerialized]
        Vector2 _DraggingCanvasOffset;

        /// <summary>
        /// Open Dialogue Editor Window
        /// </summary>
        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow() => GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");

        /// <summary>
        /// Opens Dialogue in Dialogue Editor Window, if window isn't open opens window as well
        /// </summary>
        /// <param name="instanceID"> Instance of Scriptable Object </param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(1)]
        public static bool OpenDialogue(int instanceID, int line)
        {
            Dialogue _Dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;

            if(_Dialogue != null)
            {
                ShowEditorWindow();                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Does something if different SO is selected
        /// GUIStyle is defined in this Method
        /// </summary>
        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;          

            //Defines Node Style
            _NodeStyle = new GUIStyle();
            _NodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            _NodeStyle.padding = new RectOffset(20, 20, 20, 20);
            _NodeStyle.border = new RectOffset(12, 12, 12, 12);

            _PlayerNodeStyle = new GUIStyle();
            _PlayerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            _PlayerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            _PlayerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        /// <summary>
        /// If a different Dialogue is Selected set _SelectedDialogue to this Dialogue + Call OnGUI.
        /// </summary>
        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            
            if(newDialogue != null)
            {
                _SelectedDialogue = newDialogue;
                Repaint();
            }
        }

        /// <summary>
        /// "Repaints" Dialogue Window
        /// </summary>
        private void OnGUI()
        {
            if (_SelectedDialogue != null)
            {
                ProcessEvent();

                _ScrollPosition = EditorGUILayout.BeginScrollView(_ScrollPosition);

                GUILayoutUtility.GetRect(4000, 4000);                

                foreach (DialogueNode node in _SelectedDialogue.GetAllNodes)
                {
                    DrawConnections(node);
                }


                foreach (DialogueNode node in _SelectedDialogue.GetAllNodes)
                {
                    DrawNode(node);
                }               

                EditorGUILayout.EndScrollView();

                if (_CreatingNode != null)
                {                                      
                    _SelectedDialogue.CreateNode(_CreatingNode, _TypeToMake);
                    _CreatingNode = null;
                    _TypeToMake = null;
                }

                if (_DeletingNode != null)
                {                    
                    _SelectedDialogue.DeleteNode(_DeletingNode);
                    _DeletingNode = null;
                }                             
            }
        }        

        /// <summary>
        /// Handles node dragging by user
        /// </summary>
        private void ProcessEvent()
        {
            //Updates Node Position when being Dragged by User
            switch (Event.current.type)
            {
                case EventType.MouseDown when _DraggingNode == null:
                    
                    _DraggingNode = GetNodeAtPoint(Event.current.mousePosition + _ScrollPosition);

                    if (_DraggingNode != null)
                    {
                        _DraggingOffset = _DraggingNode.RectPosition - Event.current.mousePosition;
                        Selection.activeObject = _DraggingNode;
                    }
                    else 
                    {
                        _DraggingCanvas = true;
                        _DraggingCanvasOffset = Event.current.mousePosition + _ScrollPosition;
                        Selection.activeObject = _SelectedDialogue;
                    }

                    break;

                case EventType.MouseDrag when _DraggingNode != null:

                    _DraggingNode.SetPosition(Event.current.mousePosition + _DraggingOffset);                   
                    GUI.changed = true;

                    break;

                case EventType.MouseDrag when _DraggingCanvas:

                    _ScrollPosition = _DraggingCanvasOffset - Event.current.mousePosition;

                    GUI.changed = true;

                    break;

                case EventType.MouseUp when _DraggingCanvas:

                    _DraggingCanvas = false;

                    break;

                case EventType.MouseUp when _DraggingNode != null:
                    
                    _DraggingNode = null;

                    break;
            }
        }

        /// <summary>
        /// Makes sure all data in nodes in Displayed in Dialogue Window
        /// --> Is called in OnGUI
        /// </summary>
        /// <param name="node"></param>
        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = _NodeStyle;

            if (node.GetType() == typeof(PlayerNode))
            {
                style = _PlayerNodeStyle;
            }

            GUILayout.BeginArea(node.Rect, style);        

            node.SetText(EditorGUILayout.TextField(node.Text));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Player"))
            {
                _CreatingNode = node;
                _TypeToMake = "Player";
            }

            if (GUILayout.Button("Add NPC"))
            {
                _CreatingNode = node;
                _TypeToMake = "NPC";
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            DrawLinkButtons(node);            

            if (GUILayout.Button("Delete"))
            {
                _DeletingNode = node;
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }     
       
        /// <summary>
        /// Handles Node Linking
        /// </summary>
        /// <param name="node"></param>
        private void DrawLinkButtons(DialogueNode node)
        {
            if (_LinkingParent == null)
            {
                if (GUILayout.Button("Link"))
                {
                    _LinkingParent = node;
                }
            }

            else if(_LinkingParent == node)
            {
                if (GUILayout.Button("Cancel"))
                {
                    _LinkingParent = null;
                }
            }  
            
            else if (_LinkingParent.Children.Contains(node.name))
            {
                if (GUILayout.Button("Unlink"))
                {                   
                    _LinkingParent.RemoveChild(node.name);
                    _LinkingParent = null;
                }
            }

            else
            {
                if (GUILayout.Button("Child"))
                {               
                    if (!_LinkingParent.Children.Contains(node.name))
                    {
                        _LinkingParent.AddChild(node.name);
                    }

                    _LinkingParent = null;
                }
            }
        }

        /// <summary>
        /// Draws all Bezier Connections between nodes
        /// </summary>
        /// <param name="node"></param>
        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.Rect.xMax, node.Rect.center.y);            

            foreach (DialogueNode childNode in _SelectedDialogue.GetAllChildren(node))
            {                
                Vector3 endPosition = new Vector2(childNode.Rect.xMin, childNode.Rect.center.y);
                
                //Vector for Bezier Curve Curve
                Vector3 controllPointOffset = endPosition - startPosition;
                
                //Weaken Curve by a Tiny Bit to make it look better
                controllPointOffset.y = 0;
                controllPointOffset.x *= 0.8f;
                
                //Draw Brezier Curves
                Handles.DrawBezier(startPosition, endPosition, 
                    startPosition + controllPointOffset, 
                    endPosition - controllPointOffset, 
                    Color.gray, null, 4f);
            }
        }

        /// <summary>
        /// Checks if player is clothing on position of node if so return this so that node can be dragged
        /// Returns Null of no node was found
        /// --> Used in OnGUI
        /// </summary>
        /// <param name="mousePosition">Mouse Position to determen overlaping with Node</param>
        /// <returns></returns>
        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            
            //Goes trough all nodes and checks which are at the clicked position
            //Found node is stored in varieble to make sure Top Node in Dialogue Editor is Selected
            foreach (DialogueNode node in _SelectedDialogue.GetAllNodes)
            {
                if (node.Rect.Contains(point))
                {
                    foundNode = node;
                }
            }

            return foundNode;
        }       
    }   
}


