using UnityEngine;

public class SwordEnemy : EnemyBehaviourWithWeapon
{
    protected override void InitializeStates()
    {
        base.InitializeStates();

        stateMachine.AddState(new EnemyStateSwordAttacking(this, stateMachine, player));
        
        stateMachine.AddState(new EnemyStateMeleeRetreat(this, stateMachine, player));
    }
}