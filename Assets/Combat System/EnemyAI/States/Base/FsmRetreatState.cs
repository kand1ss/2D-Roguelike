using NavMeshPlus.Extensions;
using UnityEngine;

public abstract class FsmRetreatState : FsmState
{
    protected readonly IEnemyAI enemy;
    protected readonly Player target;

    private readonly float initSpeed;

    protected readonly float retreatStartDistance;
    private readonly float retreatSpeed;
    
    protected float DistanceToTarget => Vector3.Distance(enemy.transform.position, target.transform.position);
    
    public FsmRetreatState(IEnemyAI enemy, Fsm stateMachine, Player target, float startDistance, float retreatSpeed) : base(stateMachine)
    {
        this.enemy = enemy;
        this.target = target;
        retreatStartDistance = startDistance;
        this.retreatSpeed = retreatSpeed;

        initSpeed = enemy.Agent.speed;
    }

    public override void Enter()
    {
        Debug.Log("FLEE STATE: [ENTER]");
        enemy.Agent.speed = retreatSpeed;
    }

    public override void Update()
    {
        HandleFleeState();
    }

    private void HandleFleeState()
    {
        var fleeDirection = enemy.transform.position - target.transform.position;
        enemy.Agent.SetDestination(fleeDirection);
    }

    public override void Exit()
    {
        Debug.Log("FLEE STATE: [EXIT]");
        enemy.Agent.speed = initSpeed;
    }
}