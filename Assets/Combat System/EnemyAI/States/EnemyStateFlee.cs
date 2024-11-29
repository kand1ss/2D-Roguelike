using NavMeshPlus.Extensions;
using UnityEngine;

public class EnemyStateFlee : FsmState
{
    private readonly IEnemyAI enemy;
    private readonly Player target;

    private readonly float initSpeed;

    private readonly float fleeStartDistance;
    private readonly float fleeSpeed;
    
    float DistanceToTarget => Vector3.Distance(enemy.transform.position, target.transform.position);
    
    public EnemyStateFlee(IEnemyAI enemy, Fsm stateMachine, Player target, float startDistance, float fleeSpeed) : base(stateMachine)
    {
        this.enemy = enemy;
        this.target = target;
        fleeStartDistance = startDistance;
        this.fleeSpeed = fleeSpeed;

        initSpeed = enemy.Agent.speed;
    }

    public override void Enter()
    {
        Debug.Log("FLEE STATE: [ENTER]");
        enemy.Agent.speed = fleeSpeed;
    }

    public override void Update()
    {
        HandleFleeState();
        ChasingStateTransition();
        
        if(enemy is IEnemyWithWeapon enemyWeapon)
            enemyWeapon.WeaponController.UseChosenWeapon();
    }

    private void HandleFleeState()
    {
        if (DistanceToTarget < fleeStartDistance)
        {
            var fleeDirection = enemy.transform.position - target.transform.position;
            enemy.Agent.SetDestination(fleeDirection);
        }
    }

    private void ChasingStateTransition()
    {
        if(DistanceToTarget > fleeStartDistance)
            StateMachine.SetState<EnemyStateChasing>();
    }

    public override void Exit()
    {
        Debug.Log("FLEE STATE: [EXIT]");
        enemy.Agent.speed = initSpeed;
    }
}