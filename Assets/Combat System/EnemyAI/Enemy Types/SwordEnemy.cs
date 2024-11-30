using UnityEngine;

public class SwordEnemy : EnemyBehaviourWithWeapon
{
    protected override void InitializeStates()
    {
        base.InitializeStates();
        
        stateMachine.AddState(new EnemyStateSwordAttacking(this, stateMachine, player, attackingStartDistance, attackInterval));
    }

    protected override void AttackingStateTransition()
    {
        if (stateMachine.CurrentState is not FsmAttackingState)
        {
            if (DistanceToPlayer <= attackingStartDistance)
                stateMachine.SetState<EnemyStateSwordAttacking>();
        }
    }
}