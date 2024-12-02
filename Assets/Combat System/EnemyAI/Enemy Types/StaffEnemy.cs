using UnityEngine;
using UnityEngine.Serialization;

public class StaffEnemy : EnemyBehaviourWithWeapon
{
    [SerializeField] protected float retreatStartDistance;

    protected override void InitializeStates()
    {
        base.InitializeStates();
        
        stateMachine.AddState(new EnemyStateStaffAttacking(this, stateMachine, player, attackingStartDistance, attackInterval));
        stateMachine.AddState(new EnemyStateRangeRetreat(this, stateMachine, player, retreatStartDistance, retreatSpeed, attackingStartDistance));
    }

    protected override bool CheckRetreatStateCondition()
    {
        if (!(stateMachine.CurrentState is FsmRetreatState))
        {
            if (DistanceToPlayer <= retreatStartDistance)
                return true;
        }

        return false;
    }
}