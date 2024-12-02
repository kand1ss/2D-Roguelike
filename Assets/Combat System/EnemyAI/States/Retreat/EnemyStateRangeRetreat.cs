public class EnemyStateRangeRetreat : FsmRetreatState
{
    private float attackingStartDistance;
    public EnemyStateRangeRetreat(IEnemyAI enemy, Fsm stateMachine, Player target, float startDistance, float retreatSpeed, float attackingDistance) : base(enemy, stateMachine, target, startDistance, retreatSpeed)
    {
        attackingStartDistance = attackingDistance;
    }

    public override void Update()
    {
        base.Update();
        
        if(enemy is IEnemyWithWeapon enemyWeapon)
            enemyWeapon.WeaponController.UseChosenWeapon();
        
        AttackingStateTransition();
    }
    
    private void AttackingStateTransition()
    {
        if(DistanceToTarget > retreatStartDistance)
            StateMachine.SetState<FsmAttackingState>();
    }
}