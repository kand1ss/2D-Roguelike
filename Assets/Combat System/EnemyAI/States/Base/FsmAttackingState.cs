public abstract class FsmAttackingState : FsmState
{
    protected readonly Enemy enemy;
    protected readonly Player target;

    protected readonly float attackInterval;
    protected float attackTimer;
    
    protected readonly float attackingStartDistance;
    
    protected FsmAttackingState(Enemy enemy, Fsm fsm, Player player, float attackDistance, float attackInterval) :
        base(fsm)
    {
        this.enemy = enemy;
        target = player;

        attackingStartDistance = attackDistance;
        this.attackInterval = attackInterval;

        attackTimer = 0;
    }
    
    protected void ChasingStateTransition()
    {
        if (enemy.DistanceToPlayer > attackingStartDistance)
            Fsm.SetState<EnemyStateChasing>();
    }
}