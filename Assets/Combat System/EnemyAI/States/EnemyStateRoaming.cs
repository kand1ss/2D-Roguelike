using UnityEngine;
using UnityEngine.AI;

public class EnemyStateRoaming : FsmState
{
    private readonly IEnemyAI enemyAi;
    private readonly EnemyAISettings enemySettings;

    private readonly float roamingMaxDistance;
    private readonly float roamingMinDistance;
    private readonly float roamingSpeed;

    private readonly float initSpeed;

    private readonly float roamingMaxTime = 4f;
    private float roamingTimer;
    
    private Vector3 startPosition;
    private Vector3 roamPosition;

    public EnemyStateRoaming(IEnemyAI enemyAi, Fsm stateMachine) : base(stateMachine)
    {
        this.enemyAi = enemyAi;
        enemySettings = enemyAi.AiSettings;

        initSpeed = enemyAi.Agent.speed;
        
        roamingMaxDistance = enemySettings.roamingDistanceMax;
        roamingMinDistance = enemySettings.roamingDistanceMin;
        roamingSpeed = enemySettings.roamingSpeed;
    }

    public override void Enter()
    {
        Debug.Log("Roaming State: [ENTER]");
        
        startPosition = enemyAi.Agent.transform.position;
        enemyAi.Agent.speed = roamingSpeed;
        roamingTimer = roamingMaxTime;
        
        Roaming();
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        enemyAi.Agent.SetDestination(roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + GetRandomDirection() * Random.Range(roamingMinDistance, roamingMaxDistance);
    }

    private Vector3 GetRandomDirection()
    {
        return new Vector3(
            UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public override void Update()
    {
        CheckStateTransitions();
    }

    private void CheckStateTransitions()
    {
        ChasingStateTransition();
        IdleStateTransition();
    }

    private void IdleStateTransition()
    {
        roamingTimer -= Time.deltaTime;
        if (Vector3.Distance(enemyAi.transform.position, roamPosition) < 0.1f || roamingTimer <= 0)
            StateMachine.SetState<EnemyStateIdle>();
    }

    private void ChasingStateTransition()
    {
        var chasingDistance = enemySettings.chasingStartDistance;
        
        if(enemyAi.DistanceToPlayer < chasingDistance && enemyAi.CanSeePlayer())
            StateMachine.SetState<EnemyStateChasing>();
    }

    public override void Exit()
    {
        Debug.Log("Roaming State: [EXIT]");
        enemyAi.Agent.speed = initSpeed;
    }
}