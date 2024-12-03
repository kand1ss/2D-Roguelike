using System;
using UnityEngine;

public abstract class DefaultEnemyBehaviour : EnemyAI
{
    protected override void Start()
    {
        base.Start();
        
        InitializeStates();
    }

    protected virtual void InitializeStates()
    {
        stateMachine.AddState(new EnemyStateIdle(this, stateMachine));
        stateMachine.AddState(new EnemyStateRoaming(this, stateMachine));
        stateMachine.AddState(new EnemyStateChasing(this, stateMachine, player));
        
        stateMachine.SetState<EnemyStateIdle>();
    }
    
    protected override void Update()
    {
        base.Update();
        
        stateTransitionsManager.Update();
    }

    protected virtual void OnDestroy()
    {
        stateMachine.CurrentState.Exit();
    }
}