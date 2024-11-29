using UnityEngine;

public class StaffEnemy : EnemyBehaviourWithWeapon
{
    [SerializeField] private float fleeStartDistance;
    [SerializeField] private float fleeSpeed;
    
    protected override void InitializeStates()
    {
        base.InitializeStates();
        
        stateMachine.AddState(new EnemyStateStaffAttacking(this, stateMachine, player, attackingStartDistance, attackInterval));
        stateMachine.AddState(new EnemyStateFlee(this, stateMachine, player, fleeStartDistance, fleeSpeed));
    }

    protected override void CheckTransitionsFromAnyState()
    {
        base.CheckTransitionsFromAnyState();
        
        FleeStateTransition();
    }

    protected override void AttackingStateTransition()
    {
        if (!(stateMachine.CurrentState is FsmAttackingState) && !(stateMachine.CurrentState is EnemyStateFlee))
        {
            if (DistanceToPlayer <= attackingStartDistance)
            {
                stateMachine.SetState<EnemyStateStaffAttacking>();
            }
        }
    }

    private void FleeStateTransition()
    {
        if (stateMachine.CurrentState is not EnemyStateFlee)
        {
            if (DistanceToPlayer <= fleeStartDistance)
                stateMachine.SetState<EnemyStateFlee>();
        }
    }
}