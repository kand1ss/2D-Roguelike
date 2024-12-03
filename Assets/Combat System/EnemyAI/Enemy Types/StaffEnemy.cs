using UnityEngine;
using UnityEngine.Serialization;

public class StaffEnemy : EnemyBehaviourWithWeapon
{
    protected override void InitializeStates()
    {
        base.InitializeStates();

        stateMachine.AddState(new EnemyStateStaffAttacking(this, stateMachine, player));
        stateMachine.AddState(new EnemyStateRangeRetreat(this, stateMachine, player));
    }
}