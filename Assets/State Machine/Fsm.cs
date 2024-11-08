using System;
using System.Collections.Generic;
using UnityEngine;

public class Fsm
{
    public FsmState CurrentState { get; private set; }
    
    private Dictionary<Type, FsmState> states = new Dictionary<Type, FsmState>();

    public void AddState(FsmState newState)
    {
        states.Add(newState.GetType(), newState);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);
        
        if (CurrentState?.GetType() == type)
            return;

        if (states.TryGetValue(type, out FsmState newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}