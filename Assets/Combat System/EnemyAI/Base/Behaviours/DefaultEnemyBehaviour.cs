using System;
using UnityEngine;

public abstract class DefaultEnemyBehaviour : EnemyAI
{
    [SerializeField] protected float idleTime;
    
    [SerializeField] protected float chasingStartDistance;
    [SerializeField] protected float chasingSpeed;
    
    [SerializeField] protected float roamingDistanceMin;
    [SerializeField] protected float roamingDistanceMax;
    [SerializeField] protected float roamingSpeed;

    protected override void Start()
    {
        base.Start();
        
        InitializeStates();
    }

    protected virtual void InitializeStates()
    {
        stateMachine.AddState(new EnemyStateIdle(this, stateMachine, idleTime));
        stateMachine.AddState(new EnemyStateRoaming(this, stateMachine, roamingDistanceMax, roamingDistanceMin, roamingSpeed));
        stateMachine.AddState(new EnemyStateChasing(this, stateMachine, player, chasingStartDistance, chasingSpeed));
        
        stateMachine.SetState<EnemyStateIdle>();
    }
    
    protected override void Update()
    {
        base.Update();
        
        CheckTransitionsFromAnyState();
    }

    protected virtual void CheckTransitionsFromAnyState()
    {
        ChasingStateTransition();
    }
    
    protected virtual void ChasingStateTransition()
    {
        if (stateMachine.CurrentState is EnemyStateRoaming or EnemyStateIdle)
        {
            if (DistanceToPlayer <= chasingStartDistance && CanSeePlayer())
                stateMachine.SetState<EnemyStateChasing>();
        }
    }

    protected virtual void OnDestroy()
    {
        stateMachine.CurrentState.Exit();
    }
}