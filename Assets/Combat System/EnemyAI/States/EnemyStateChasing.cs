using UnityEngine;
using UnityEngine.AI;

public class EnemyStateChasing : FsmState
{
    private readonly IEnemyAI enemy;
    private readonly Player target;

    private readonly float chasingStartDistance;
    private readonly float chasingSpeed;

    public EnemyStateChasing(IEnemyAI enemy, Fsm stateMachine, Player target, float startDistance, float chasingSpeed) : base(stateMachine)
    {
        this.enemy = enemy;
        this.target = target;

        chasingStartDistance = startDistance;
        this.chasingSpeed = chasingSpeed;
    }

    public override void Enter()
    {
        Debug.Log("Chasing State: [ENTER]");

        enemy.Agent.speed = 5f;
    }

    public override void Update()
    {
        Chasing();
        IdleStateTransition();
    }

    private void Chasing()
    {
        enemy.Agent.SetDestination(target.transform.position);
    }
    
    private void IdleStateTransition()
    {
        var distanceToPlayer = Vector3.Distance(target.transform.position, enemy.transform.position);
        if (distanceToPlayer > chasingStartDistance)
            StateMachine.SetState<EnemyStateIdle>();
    }

    public override void Exit()
    {
        Debug.Log("Chasing State: [EXIT]");
    }
}