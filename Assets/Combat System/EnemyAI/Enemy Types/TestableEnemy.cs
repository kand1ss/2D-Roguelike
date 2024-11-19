using UnityEngine;

public class TestableEnemy : EnemyAI, IEnemyWithWeapon
{
    [SerializeField] private float idleTime;

    [SerializeField] private float roamingDistanceMin;
    [SerializeField] private float roamingDistanceMax;
    [SerializeField] private float roamingSpeed;

    [SerializeField] private float chasingStartDistance;
    [SerializeField] private float chasingSpeed;

    [SerializeField] private float attackingStartDistance;
    [SerializeField] private float attackInterval;

    [field: SerializeField] public EnemyWeaponController WeaponController { get; private set; }

    protected override void Start()
    {
        base.Start();

        InitializeStates();
    }

    private void InitializeStates()
    {
        stateMachine.AddState(new EnemyStateIdle(this, stateMachine, idleTime));
        stateMachine.AddState(new EnemyStateRoaming(this, stateMachine, roamingDistanceMax, roamingDistanceMin, roamingSpeed));
        stateMachine.AddState(new EnemyStateChasing(this, stateMachine, player, chasingStartDistance, chasingSpeed));
        
        if (WeaponController.ChosenWeapon is Sword)
            stateMachine.AddState(new EnemyStateSwordAttacking(this, stateMachine, player, attackingStartDistance, attackInterval));
        else if (WeaponController.ChosenWeapon is Staff)
            stateMachine.AddState(new EnemyStateStaffAttacking(this, stateMachine, player, attackingStartDistance, attackInterval));

        stateMachine.SetState<EnemyStateIdle>();
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
        if (stateMachine.CurrentState is EnemyStateRoaming || stateMachine.CurrentState is EnemyStateIdle)
        {
            if (DistanceToPlayer <= chasingStartDistance && CanSeePlayer())
                stateMachine.SetState<EnemyStateChasing>();
        }
    }

    private void AttackingStateTransition()
    {
        if (stateMachine.CurrentState is not FsmAttackingState)
        {
            if (DistanceToPlayer <= attackingStartDistance)
            {
                if (WeaponController.ChosenWeapon is Sword)
                    stateMachine.SetState<EnemyStateSwordAttacking>();
                else if (WeaponController.ChosenWeapon is Staff)
                    stateMachine.SetState<EnemyStateStaffAttacking>();
            }
        }
    }

    private void OnDestroy()
    {
        stateMachine.CurrentState.Exit();
        WeaponController.ChosenWeapon.DetachWeapon();
    }
}