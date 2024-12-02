public class EnemyStateMeleeRetreat : FsmRetreatState
{
    private float attackingStartDistance;
    public EnemyStateMeleeRetreat(IEnemyAI enemy, Fsm stateMachine, Player target, float startDistance, float retreatSpeed, float attackingDistance) : base(enemy, stateMachine, target, startDistance, retreatSpeed)
    {
        attackingStartDistance = attackingDistance;
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
        
        if(enemy is IEnemyWithWeapon enemyWeapon)
            enemyWeapon.WeaponController.UseChosenWeapon();
    }

    private void RoamingStateTransition()
    {
        var retreatDistanceMultiplier = 3;
        
        if (DistanceToTarget > retreatStartDistance * retreatDistanceMultiplier)
            StateMachine.SetState<EnemyStateRoaming>();
    }
}