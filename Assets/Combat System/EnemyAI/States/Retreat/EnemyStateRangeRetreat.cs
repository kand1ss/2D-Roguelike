public class EnemyStateRangeRetreat : FsmRetreatState
{
    private float attackingStartDistance;
    public EnemyStateRangeRetreat(IEnemyAI enemyAi, Fsm stateMachine, Player target) : base(enemyAi, stateMachine, target)
    {
        attackingStartDistance = enemySettings.attackingStartDistance;
    }

    public override void Update()
    {
        base.Update();
        
        if(enemyAi is IEnemyWithWeapon enemyWeapon)
            enemyWeapon.WeaponController.UseChosenWeapon();
        
        AttackingStateTransition();
    }
    
    private void AttackingStateTransition()
    {
        if(DistanceToTarget > retreatStartDistance)
            StateMachine.SetState<FsmAttackingState>();
    }
}