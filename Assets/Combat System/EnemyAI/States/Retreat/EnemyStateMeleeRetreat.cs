public class EnemyStateMeleeRetreat : FsmRetreatState
{
    private float attackingStartDistance;
    public EnemyStateMeleeRetreat(IEnemyAI enemyAi, Fsm stateMachine, Player target) : base(enemyAi, stateMachine, target)
    {
        attackingStartDistance = enemySettings.attackingStartDistance;
    }

    public override void Update()
    {
        base.Update();
        
        UseWeaponIfDistanceShort();
        RoamingStateTransition();
    }

    private void UseWeaponIfDistanceShort()
    {
        if (!(DistanceToTarget < attackingStartDistance)) 
            return;
        
        if(enemyAi is IEnemyWithWeapon enemyWeapon)
            enemyWeapon.WeaponController.UseChosenWeapon();
    }

    private void RoamingStateTransition()
    {
        var retreatDistanceMultiplier = 3;
        
        if (DistanceToTarget > retreatStartDistance * retreatDistanceMultiplier)
            StateMachine.SetState<EnemyStateRoaming>();
    }
}