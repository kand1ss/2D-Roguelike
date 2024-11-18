using UnityEngine;

public class EnemyStateIdle : FsmState
{
    private readonly IEnemyAI enemy;

    private float idleStateMaxTime;
    private float idleStateTimer;

    public EnemyStateIdle(IEnemyAI enemy, Fsm stateMachine, float idleTime) : base(stateMachine)
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
        CheckStateTransitions();
    }

    private void CheckStateTransitions()
    {
        idleStateTimer -= Time.deltaTime;
        if (idleStateTimer <= 0)
            RoamingStateTransition();
    }

    private void RoamingStateTransition()
    {
        StateMachine.SetState<EnemyStateRoaming>();
    }

    public override void Exit()
    {
        Debug.Log("Idle State: [EXIT]");
    }
}