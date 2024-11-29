public abstract class FsmAttackingState : FsmState
{
    protected readonly EnemyAI enemyAI;
    protected readonly Player target;

    protected readonly float attackInterval;
    protected float attackTimer;
    
    private readonly float attackingStartDistance;
    
    protected FsmAttackingState(EnemyAI enemyAI, Fsm stateMachine, Player player, float attackDistance, float attackInterval) :
        base(stateMachine)
    {
        this.enemyAI = enemyAI;
        target = player;

        attackingStartDistance = attackDistance;
        this.attackInterval = attackInterval;

        attackTimer = 0;
    }
    
    protected void ChasingStateTransition()
    {
        if (enemyAI.DistanceToPlayer > attackingStartDistance)
            StateMachine.SetState<EnemyStateChasing>();
    }
}