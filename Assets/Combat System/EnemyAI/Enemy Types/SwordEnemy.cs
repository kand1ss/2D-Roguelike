using UnityEngine;

public class SwordEnemy : EnemyBehaviourWithWeapon
{
    protected override void InitializeStates()
    {
        base.InitializeStates();
        
        stateMachine.AddState(new EnemyStateSwordAttacking(this, stateMachine, player, attackingStartDistance, attackInterval));
        stateMachine.AddState(new EnemyStateMeleeRetreat(this, stateMachine, player, chasingStartDistance, retreatSpeed, attackingStartDistance));
    }

    protected override bool CheckRetreatStateCondition()
    {
        if (!(stateMachine.CurrentState is FsmRetreatState))
        {
            if(DistanceToPlayer <= chasingStartDistance)
                if (StatsManager.CurrentHealth < StatsManager.MaxHealth / 4)
                    return true;
        }

        return false;
    }
}