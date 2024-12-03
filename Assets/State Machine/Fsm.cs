using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fsm
{
    public FsmState CurrentState { get; private set; }
    private readonly TransitionMap _transitionMap;
    
    private readonly Dictionary<Type, FsmState> states = new Dictionary<Type, FsmState>();

    public Fsm()
    {
        _transitionMap = new(this);
    }

    public void AddState(FsmState newState)
    {
        var stateType = newState.GetType();
        if (states.ContainsKey(stateType))
            return;
        
        states.Add(newState.GetType(), newState);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);
        
        if (CurrentState?.GetType() == type)
            return;
        
        if(CurrentState != null)
            if (!_transitionMap.CanTransitionTo<T>())
             return;

        if (states.TryGetValue(type, out var newState))
        {
            TransitionToState(newState);
            return;
        }
        
        var compatibleState = states.Values.FirstOrDefault(state => type.IsAssignableFrom(state.GetType()));
        if(compatibleState != null)
            TransitionToState(compatibleState);
    }

    private void TransitionToState(FsmState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void AddTransitionFrom<TTo>(Type toState) where TTo : FsmState
    {
        _transitionMap.AddTransitionFrom<TTo>(toState);
    }
    public void AddTransitionsFrom<TFrom>(IEnumerable<Type> toStatesList) where TFrom : FsmState
    {
        _transitionMap.AddTransitionsFrom<TFrom>(toStatesList);
    }

    public void AddTransitionTo<TTo>(Type fromState) where TTo : FsmState
    {
        _transitionMap.AddTransitionTo<TTo>(fromState);
    }
    public void AddTransitionsTo<TTo>(IEnumerable<Type> fromStatesList) where TTo : FsmState
    {
        _transitionMap.AddTransitionsTo<TTo>(fromStatesList);
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}