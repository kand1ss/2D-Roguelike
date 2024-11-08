using UnityEngine;
using UnityEngine.AI;

public class EnemyStateRoaming : FsmState
{
    private readonly IEnemyAI enemy;

    private float roamingMaxDistance;
    private float roamingMinDistance;

    private float roamingMaxTime = 4f;
    private float roamingTimer;
    
    private Vector3 startPosition;
    private Vector3 roamPosition;

    public EnemyStateRoaming(IEnemyAI enemy, Fsm fsm, float roamDistanceMax, float roamDistanceMin) : base(fsm)
    {
        this.enemy = enemy;
        roamingMaxDistance = roamDistanceMax;
        roamingMinDistance = roamDistanceMin;
    }

    public override void Enter()
    {
        Debug.Log("Roaming State: [ENTER]");
        
        startPosition = enemy.agent.transform.position;
        enemy.agent.speed = 3f;
        roamingTimer = roamingMaxTime;
        
        Roaming();
    }

    public override void Update()
    {
        IdleStateTransition();
    }

    private void IdleStateTransition()
    {
        roamingTimer -= Time.deltaTime;
        if (Vector3.Distance(enemy.transform.position, roamPosition) < 0.1f || roamingTimer <= 0)
        {
            Fsm.SetState<EnemyStateIdle>();
        }
    }

    public override void Exit()
    {
        Debug.Log("Roaming State: [EXIT]");
        
        enemy.agent.ResetPath();
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        enemy.agent.SetDestination(roamPosition);
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
}