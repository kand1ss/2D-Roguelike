using UnityEngine;
using UnityEngine.AI;

public abstract class FsmRetreatState : FsmState
{
    protected readonly IEnemyAI enemyAi;
    protected readonly EnemyAISettings enemySettings;
    
    private readonly CharacterStatsManager statsManager;

    protected readonly Player target;

    private readonly float initSpeed;

    protected readonly float retreatStartDistance;
    private readonly float retreatSpeed;
    
    protected float DistanceToTarget => Vector3.Distance(enemyAi.transform.position, target.transform.position);
    
    protected FsmRetreatState(IEnemyAI enemyAi, Fsm stateMachine, Player target) : base(stateMachine)
    {
        this.enemyAi = enemyAi;
        enemySettings = enemyAi.AiSettings;
        
        if(enemyAi is EnemyAI enemy)
            statsManager = enemy.StatsManager;
        
        this.target = target;
        retreatStartDistance = enemySettings.retreatStartDistance;
        retreatSpeed = enemySettings.retreatSpeed;

        initSpeed = enemyAi.Agent.speed;
    }

    public override void Enter()
    {
        Debug.Log("FLEE STATE: [ENTER]");
        
        statsManager.OnHealthChanged += AttackingStateTransition;
        
        enemyAi.Agent.speed = retreatSpeed;
    }

    private void AttackingStateTransition(float health)
    {
        var retreatHealth = statsManager.MaxHealth / 4;
        if (health > retreatHealth)
            StateMachine.SetState<FsmAttackingState>();
    }

    public override void Update()
    {
        HandleState();
    }

    private void HandleState()
    {
        var aiAgent = enemyAi.Agent;
        
        var retreatDirection = (enemyAi.transform.position - target.transform.position).normalized;
        var retreatDestination = enemyAi.transform.position + retreatDirection * (retreatStartDistance * 3);

        aiAgent.SetDestination(retreatDestination);
    }

    public override void Exit()
    {
        Debug.Log("FLEE STATE: [EXIT]");
        
        statsManager.OnHealthChanged -= AttackingStateTransition;
        
        enemyAi.Agent.speed = initSpeed;
    }
}