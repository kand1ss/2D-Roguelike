using UnityEngine;
using UnityEngine.AI;

public class EnemyStateRoaming : FsmState
{
    private readonly EnemyAI enemyAI;

    private readonly float roamingMaxDistance;
    private readonly float roamingMinDistance;
    private readonly float roamingSpeed;

    private readonly float roamingMaxTime = 4f;
    private float roamingTimer;
    
    private Vector3 startPosition;
    private Vector3 roamPosition;

    public EnemyStateRoaming(EnemyAI enemyAI, Fsm stateMachine, float roamDistanceMax, float roamDistanceMin, float roamSpeed) : base(stateMachine)
    {
        this.enemyAI = enemyAI;
        
        roamingMaxDistance = roamDistanceMax;
        roamingMinDistance = roamDistanceMin;
        roamingSpeed = roamSpeed;
    }

    public override void Enter()
    {
        Debug.Log("Roaming State: [ENTER]");
        
        startPosition = enemyAI.Agent.transform.position;
        enemyAI.Agent.speed = 3f;
        roamingTimer = roamingMaxTime;
        
        Roaming();
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        enemyAI.Agent.SetDestination(roamPosition);
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
        IdleStateTransition();
    }

    private void IdleStateTransition()
    {
        roamingTimer -= Time.deltaTime;
        if (Vector3.Distance(enemyAI.transform.position, roamPosition) < 0.1f || roamingTimer <= 0)
            StateMachine.SetState<EnemyStateIdle>();
    }

    public override void Exit()
    {
        Debug.Log("Roaming State: [EXIT]");
    }
}