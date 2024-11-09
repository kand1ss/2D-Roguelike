using System;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class TestableEnemy : Enemy, IEnemyWithWeapon 
{
    [SerializeField] private float idleTime;
    
    [SerializeField] private float roamingDistanceMin;
    [SerializeField] private float roamingDistanceMax;

    [SerializeField] private float chasingStartDistance;
    [SerializeField] private float attackingStartDistance;

    [SerializeField] private Player player;
    

    [SerializeField] private EnemyWeaponController weaponController;
    public EnemyWeaponController WeaponController => weaponController;

    float distanceToPlayer => Vector3.Distance(player.transform.position, transform.position);


    protected override void Start()
    {
        base.Start();
        
        fsm.AddState(new EnemyStateIdle(this, fsm, idleTime));
        fsm.AddState(new EnemyStateRoaming(this, fsm, roamingDistanceMax, roamingDistanceMin));
        fsm.AddState(new EnemyStateChasing(this, fsm, player, chasingStartDistance));
        fsm.AddState(new EnemyStateAttacking(this, fsm, player, attackingStartDistance));
        
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
        if (distanceToPlayer <= chasingStartDistance)
            fsm.SetState<EnemyStateChasing>();
    }

    private void AttackingStateTransition()
    {
        if (distanceToPlayer <= attackingStartDistance)
            fsm.SetState<EnemyStateAttacking>();
    }

    private void OnDestroy()
    {
        weaponController.ChosenWeapon.DetachWeapon();
    }
}