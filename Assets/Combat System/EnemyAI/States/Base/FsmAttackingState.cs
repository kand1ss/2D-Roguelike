public abstract class FsmAttackingState : FsmState
{
    protected readonly EnemyAI enemyAI;
    protected readonly EnemyAISettings enemySettings;
    
    protected readonly Player target;

    protected readonly float attackInterval;
    protected float attackTimer;
    
    protected readonly float attackingStartDistance;

    protected FsmAttackingState(EnemyAI enemyAI, Fsm stateMachine, Player player) :
        base(stateMachine)
    {
        this.enemyAI = enemyAI;

        enemySettings = enemyAI.AiSettings;
        
        target = player;

        attackingStartDistance = enemySettings.attackingStartDistance;
        this.attackInterval = enemySettings.attackInterval;

        attackTimer = 0;
    }

    public override void Update()
    {
        ChasingStateTransition();
        SuspicionStateTransition();
    }

    private void SuspicionStateTransition()
    {
        if (!enemyAI.CanSeePlayer())
            StateMachine.SetState<EnemyStateSuspicion>();
    }
    private void ChasingStateTransition()
    {
        if (enemyAI.DistanceToPlayer > attackingStartDistance)
            StateMachine.SetState<EnemyStateChasing>();
    }
}