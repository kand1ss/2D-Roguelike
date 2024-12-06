using UnityEngine;
using UnityEngine.AI;

public class EnemyStateChasing : FsmState
{
    private readonly IEnemyAI enemyAI;
    private readonly EnemyAISettings enemySettings;
    
    private readonly Player target;

    private readonly float initSpeed;

    private readonly float chasingStartDistance;
    private readonly float chasingSpeed;

    public EnemyStateChasing(IEnemyAI enemyAI, Fsm stateMachine, Player target) : base(stateMachine)
    {
        this.enemyAI = enemyAI;
        enemySettings = enemyAI.AiSettings;
        
        this.target = target;

        initSpeed = enemyAI.Agent.speed;

        chasingStartDistance = enemySettings.chasingStartDistance;
        this.chasingSpeed = enemySettings.chasingSpeed;
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
        SuspicionStateTransition();
        RetreatStateTransition();
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

    private void RetreatStateTransition()
    {
        var retreatDistance = enemySettings.chasingStartDistance;
        var statsManager = ((EnemyAI)enemyAI).StatsManager;
        
        if (enemyAI.DistanceToPlayer <= retreatDistance && enemyAI.CanSeePlayer())
            if (statsManager.CurrentHealth < statsManager.MaxHealth / 4)
                StateMachine.SetState<FsmRetreatState>();
    }

    public override void Exit()
    {
        Debug.Log("Chasing State: [EXIT]");
        
        enemyAI.Agent.speed = initSpeed;
    }
}