using UnityEngine;
using UnityEngine.AI;

public class EnemyStateChasing : FsmState
{
    private readonly IEnemyAI enemy;
    private readonly Player target;

    private float chasingStartDistance;

    public EnemyStateChasing(IEnemyAI enemy, Fsm fsm, Player target, float startDistance) : base(fsm)
    {
        this.enemy = enemy;
        this.target = target;

        chasingStartDistance = startDistance;
    }

    public override void Enter()
    {
        Debug.Log("Chasing State: [ENTER]");

        enemy.agent.speed = 5f;
    }

    public override void Update()
    {
        Chasing();
        IdleStateTransition();
    }

    private void Chasing()
    {
        Debug.Log("Chasing State: [CHASING]");

        enemy.agent.SetDestination(target.transform.position);
    }

    private void IdleStateTransition()
    {
        var distanceToPlayer = Vector3.Distance(target.transform.position, enemy.transform.position);
        if (distanceToPlayer > chasingStartDistance)
            Fsm.SetState<EnemyStateIdle>();
    }

    public override void Exit()
    {
        Debug.Log("Chasing State: [EXIT]");

        enemy.agent.ResetPath();
    }
}