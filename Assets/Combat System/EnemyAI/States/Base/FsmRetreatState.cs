using NavMeshPlus.Extensions;
using UnityEngine;

public abstract class FsmRetreatState : FsmState
{
    protected readonly IEnemyAI enemyAi;

    protected readonly EnemyAISettings enemySettings;
    
    protected readonly Player target;

    private readonly float initSpeed;

    protected readonly float retreatStartDistance;
    private readonly float retreatSpeed;
    
    protected float DistanceToTarget => Vector3.Distance(enemyAi.transform.position, target.transform.position);
    
    public FsmRetreatState(IEnemyAI enemyAi, Fsm stateMachine, Player target) : base(stateMachine)
    {
        this.enemyAi = enemyAi;
        enemySettings = enemyAi.AiSettings;
        
        this.target = target;
        retreatStartDistance = enemySettings.retreatStartDistance;
        this.retreatSpeed = enemySettings.retreatSpeed;

        initSpeed = enemyAi.Agent.speed;
    }

    public override void Enter()
    {
        Debug.Log("FLEE STATE: [ENTER]");
        enemyAi.Agent.speed = retreatSpeed;
    }

    public override void Update()
    {
        HandleFleeState();
    }

    private void HandleFleeState()
    {
        var fleeDirection = enemyAi.transform.position - target.transform.position;
        enemyAi.Agent.SetDestination(fleeDirection);
    }

    public override void Exit()
    {
        Debug.Log("FLEE STATE: [EXIT]");
        enemyAi.Agent.speed = initSpeed;
    }
}