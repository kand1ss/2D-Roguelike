using UnityEngine;

public class EnemyStateIdle : FsmState
{
    private readonly IEnemyAI enemy;

    private float idleStateMaxTime;
    private float idleStateTimer;

    public EnemyStateIdle(IEnemyAI enemy, Fsm fsm, float idleTime) : base(fsm)
    {
        this.enemy = enemy;
        idleStateMaxTime = idleTime;
    }

    public override void Enter()
    {
        Debug.Log("Idle State: [ENTER]");
        idleStateTimer = idleStateMaxTime;
    }

    public override void Update()
    {
        RoamingStateTransition();
    }

    private void RoamingStateTransition()
    {
        idleStateTimer -= Time.deltaTime;
        if (idleStateTimer <= 0)
        {
            Fsm.SetState<EnemyStateRoaming>();
        }
    }

    public override void Exit()
    {
        Debug.Log("Idle State: [EXIT]");
    }
}