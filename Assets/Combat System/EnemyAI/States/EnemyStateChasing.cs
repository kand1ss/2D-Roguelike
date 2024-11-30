using UnityEngine;
using UnityEngine.AI;

public class EnemyStateChasing : FsmState
{
    private readonly IEnemyAI enemyAI;
    private readonly Player target;

    private readonly float initSpeed;

    private readonly float chasingStartDistance;
    private readonly float chasingSpeed;

    public EnemyStateChasing(IEnemyAI enemyAI, Fsm stateMachine, Player target, float startDistance, float chasingSpeed) : base(stateMachine)
    {
        this.enemyAI = enemyAI;
        this.target = target;

        initSpeed = enemyAI.Agent.speed;

        chasingStartDistance = startDistance;
        this.chasingSpeed = chasingSpeed;
    }

    public override void Enter()
    {
        Debug.Log("Chasing State: [ENTER]");

        enemyAI.Agent.speed = chasingSpeed;
    }

    public override void Update()
    {
        Chasing();
        IdleStateTransition();
    }

    private void Chasing()
    {
        enemyAI.Agent.SetDestination(target.transform.position);
    }
    
    private void IdleStateTransition()
    {
        var distanceToPlayer = Vector3.Distance(target.transform.position, enemyAI.transform.position);
        if (distanceToPlayer > chasingStartDistance)
            StateMachine.SetState<EnemyStateIdle>();
    }

    private void SuspicionStateTransition()
    {
        if(!enemyAI.CanSeePlayer())
            StateMachine.SetState<EnemyStateSuspicion>();
    }

    public override void Exit()
    {
        Debug.Log("Chasing State: [EXIT]");
        
        enemyAI.Agent.speed = initSpeed;
    }
}