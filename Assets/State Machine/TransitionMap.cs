using System;
using System.Collections.Generic;
using System.Linq;

public class TransitionMap
{
    private readonly Fsm stateMachine;
    
    private Dictionary<Type, HashSet<Type>> transitionMap = new();

    public TransitionMap(Fsm fsm)
    {
        stateMachine = fsm;
    }

    public void AddTransitionsFrom<TFrom>(IEnumerable<Type> toStatesList) where TFrom : FsmState
    {
        var fromType = typeof(TFrom);
        
        if(!transitionMap.ContainsKey(fromType))
            transitionMap[fromType] = new HashSet<Type>();

        foreach (var state in toStatesList)
        {
            if(!transitionMap[fromType].Contains(state))
                transitionMap[fromType].Add(state);
        }
    }

    public void AddTransitionFrom<TFrom>(Type toState) where TFrom : FsmState
    {
        List<Type> state = new() { toState };
        AddTransitionsFrom<TFrom>(state);
    }

    public void AddTransitionsTo<TTo>(IEnumerable<Type> fromStatesList) where TTo : FsmState
    {
        var toType = typeof(TTo);

        foreach (var fromType in fromStatesList)
        {
            if(!transitionMap.ContainsKey(fromType))
                transitionMap[fromType] = new HashSet<Type>();
            
            if(!transitionMap[fromType].Contains(toType))
                transitionMap[fromType].Add(toType);
        }
    }

    public void AddTransitionTo<TTo>(Type fromState) where TTo : FsmState
    {
        List<Type> state = new() { fromState };
        AddTransitionsTo<TTo>(state);
    }

    public bool CanTransitionTo<TTo>() where TTo : FsmState
    {
        if (stateMachine.CurrentState == null)
            return false;
        
        var toType = typeof(TTo);
        var currStateType = stateMachine.CurrentState.GetType();
        
        var compatibleKeys = transitionMap.Keys.Where(key => key.IsAssignableFrom(currStateType));

        foreach (var fromType in compatibleKeys)
        {
            if (transitionMap[fromType].Any(allowed => allowed.IsAssignableFrom(toType)))
                return true;
        }

        return false;
    }
}