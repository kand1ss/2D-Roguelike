using UnityEngine;

public class EnemyStateAttacking : FsmState
{
    private readonly Player target;
    private readonly Enemy enemy;

    private float attackingStartDistance;

    public EnemyStateAttacking(Enemy enemy, Fsm fsm, Player target, float attackDistance) : base(fsm)
    {
        this.target = target;
        this.enemy = enemy;
        attackingStartDistance = attackDistance;
    }

    public override void Enter()
    {
        Debug.Log("Attacking State: [ENTER]");
    }

    public override void Update()
    {
        CheckTransitions();
        
        if (enemy is IEnemyWithWeapon enemyWithWeapon)
            enemyWithWeapon.WeaponController.UseChosenWeapon();
    }

    private void CheckTransitions()
    {
        ChasingStateTransition();
    }

    private void ChasingStateTransition()
    {
        var distanceToPlayer = Vector3.Distance(target.transform.position, enemy.transform.position);

        if (distanceToPlayer > attackingStartDistance)
            Fsm.SetState<EnemyStateChasing>();
    }

    public override void Exit()
    {
        Debug.Log("Attacking State: [EXIT]");

        // if (enemy is IEnemyWithWeapon enemyWithWeapon)
        //     enemyWithWeapon?.WeaponController.ResetWeaponRotation();
    }
}