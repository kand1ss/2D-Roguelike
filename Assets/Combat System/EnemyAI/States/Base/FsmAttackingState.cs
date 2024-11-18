public abstract class FsmAttackingState : FsmState
{
    protected readonly EnemyAI EnemyAI;
    protected readonly Player target;

    protected readonly float attackInterval;
    protected float attackTimer;
    
    protected readonly float attackingStartDistance;
    
    protected FsmAttackingState(EnemyAI enemyAI, Fsm stateMachine, Player player, float attackDistance, float attackInterval) :
        base(stateMachine)
    {
        this.EnemyAI = enemyAI;
        target = player;

        attackingStartDistance = attackDistance;
        this.attackInterval = attackInterval;

        attackTimer = 0;
    }
    
    protected void ChasingStateTransition()
    {
        if (EnemyAI.DistanceToPlayer > attackingStartDistance)
            StateMachine.SetState<EnemyStateChasing>();
    }
}