using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Zenject.SpaceFighter;

public class TestableEnemy : Enemy, IEnemyWithWeapon
{
    [SerializeField] private float idleTime;

    [SerializeField] private float roamingDistanceMin;
    [SerializeField] private float roamingDistanceMax;

    [SerializeField] private float chasingStartDistance;

    [SerializeField] private float attackingStartDistance;
    [SerializeField] private float attackInterval;

    [field: SerializeField] public EnemyWeaponController WeaponController { get; private set; }

    protected override void Start()
    {
        base.Start();

        fsm.AddState(new EnemyStateIdle(this, fsm, idleTime));
        fsm.AddState(new EnemyStateRoaming(this, fsm, roamingDistanceMax, roamingDistanceMin));
        fsm.AddState(new EnemyStateChasing(this, fsm, player, chasingStartDistance));

        if (WeaponController.ChosenWeapon is Sword)
            fsm.AddState(new EnemyStateSwordAttacking(this, fsm, player, attackingStartDistance, attackInterval));
        else if (WeaponController.ChosenWeapon is Staff)
            fsm.AddState(new EnemyStateStaffAttacking(this, fsm, player, attackingStartDistance, attackInterval));

        fsm.SetState<EnemyStateIdle>();
    }

    protected override void Update()
    {
        base.Update();

        CheckTransitionsFromAnyState();
    }

    private void CheckTransitionsFromAnyState()
    {
        ChasingStateTransition();
        AttackingStateTransition();
    }

    private void ChasingStateTransition()
    {
        if (fsm.CurrentState is not EnemyStateChasing && fsm.CurrentState is not FsmAttackingState)
        {
            if (DistanceToPlayer <= chasingStartDistance)
                fsm.SetState<EnemyStateChasing>();
        }
    }

    private void AttackingStateTransition()
    {
        if (fsm.CurrentState is not FsmAttackingState)
        {
            if (DistanceToPlayer <= attackingStartDistance)
            {
                if (WeaponController.ChosenWeapon is Sword)
                    fsm.SetState<EnemyStateSwordAttacking>();
                else if (WeaponController.ChosenWeapon is Staff)
                    fsm.SetState<EnemyStateStaffAttacking>();
            }
        }
    }

    private void OnDestroy()
    {
        fsm.CurrentState.Exit();
        WeaponController.ChosenWeapon.DetachWeapon();
    }
}