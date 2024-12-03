using System;
using System.Collections.Generic;

public class EnemyStateTransitionsManager
{
    private readonly IEnemyAI enemyAI;
    private readonly Fsm stateMachine;
    
    private float DistanceToPlayer => enemyAI.DistanceToPlayer;
    private EnemyAISettings AiSettings => enemyAI.AiSettings;
    
    
    public EnemyStateTransitionsManager(IEnemyAI enemyAI, Fsm fsm)
    {
        this.enemyAI = enemyAI;
        stateMachine = fsm;
        
        InitializeStateTransitions();
    }

    private void InitializeStateTransitions()
    {
        stateMachine.AddTransitionsTo<EnemyStateSuspicion>( 
            new List<Type>()
            {
                typeof(EnemyStateRoaming),
                typeof(EnemyStateIdle),
                typeof(FsmAttackingState)
            });
        
        stateMachine.AddTransitionsTo<EnemyStateIdle>(
            new List<Type>()
            {
                typeof(EnemyStateRoaming),
                typeof(EnemyStateChasing),
                typeof(EnemyStateSuspicion)
            });
        
        stateMachine.AddTransitionsTo<EnemyStateRoaming>(
            new List<Type>()
            {
                typeof(EnemyStateIdle),
                typeof(FsmRetreatState)
            });
        
        stateMachine.AddTransitionsTo<EnemyStateChasing>(
            new List<Type>()
            {
                typeof(EnemyStateIdle),
                typeof(EnemyStateRoaming),
                typeof(EnemyStateSuspicion),
                typeof(FsmAttackingState),
            });
        
        stateMachine.AddTransitionsTo<FsmAttackingState>(
            new List<Type>()
            {
                typeof(EnemyStateChasing),
                typeof(FsmRetreatState)
            });
        
        stateMachine.AddTransitionsTo<FsmRetreatState>(
            new List<Type>()
            {
                typeof(FsmAttackingState),
                typeof(EnemyStateRoaming),
                typeof(EnemyStateIdle)
            });
    }

    public void Update()
    {
        AttackingStateTransition();
        ChasingStateTransition();
    }
    
    private void AttackingStateTransition()
    {
        if (stateMachine.CurrentState is FsmRetreatState)
            return;
        
        if (DistanceToPlayer <= AiSettings.attackingStartDistance)
            stateMachine.SetState<FsmAttackingState>();
    }
    
    private void ChasingStateTransition()
    {
        if (stateMachine.CurrentState is FsmAttackingState)
            return;

        var chasingDistance = AiSettings.chasingStartDistance;
        
        if(DistanceToPlayer <= chasingDistance && enemyAI.CanSeePlayer())
            stateMachine.SetState<EnemyStateChasing>();
    }
}