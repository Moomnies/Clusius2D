using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine
{
    IState _CurrentState;
    bool debugMode;

    public IState GetCurrentState() => _CurrentState;

    Dictionary<Type, List<Transition>> _Transition = new Dictionary<Type, List<Transition>>();
    Dictionary<Func<bool>, Action> transitionAction = new Dictionary<Func<bool>, Action>();

    List<Transition> _CurrentTransitions = new List<Transition>();
    List<Transition> _AnyTransition = new List<Transition>();

    static List<Transition> _EmptyTransitions = new List<Transition>(0);

    public StateMachine(bool debugMode)
    {
        this.debugMode = debugMode;
    }

    public void Tick()
    {
        if (debugMode) Debug.Log("TICK");

        Transition _Transition = GetTransition();
        if (_Transition != null)
        {
            SetState(_Transition._To);

            Action transitionAction = GetTransitionAction(_Transition);

            if (transitionAction != null)
            {
                transitionAction();
            }                
        }            

        _CurrentState?.Tick();
    }    

    Action GetTransitionAction(Transition transition)
    {
        if (transitionAction.ContainsKey(transition._Condition))
        {
            return transitionAction[transition._Condition];
        }

        return null;
    }

    public void SetState(IState state)
    {
        if (state == _CurrentState)
            return;

        if (_CurrentState != null)
        {
            if(debugMode) Debug.Log("STATE EXIT: " + _CurrentState.GetType().ToString());
            _CurrentState.OnExit();
        }
        _CurrentState = state;

        _Transition.TryGetValue(_CurrentState.GetType(), out _CurrentTransitions);
        if (_CurrentTransitions == null)
            _CurrentTransitions = _EmptyTransitions;

        if(debugMode) Debug.Log("STATE ENTER: " + _CurrentState.GetType().ToString());
        _CurrentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (_Transition.TryGetValue(from?.GetType(), out var transitions) == false)
        {
             transitions = new List<Transition>();
            _Transition[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));        
    }

    public void Pause()
    {
        _CurrentState.OnPauze();
    }

    public void Continue()
    {
        _CurrentState.OnContinue();
    }

    public void AddAnyTransition(IState state, Func<bool> predicate) => _AnyTransition.Add(new Transition(state, predicate));

    public void AddTransitionAction(Func<bool> condition, Action action, Action method)
    {
        //action += method;
        transitionAction.Add(condition, action);
    }

    class Transition
    {
        public Func<bool> _Condition { get; }
        public IState _To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            _To = to;
            _Condition = condition;
        }
    }

    Transition GetTransition()
    {
        foreach(Transition _Transition in _AnyTransition)
            if (_Transition._Condition())
                return _Transition;

        foreach (Transition _Transition in _CurrentTransitions)
            if (_Transition._Condition())
                return _Transition;
        
        return null;
    }
}
