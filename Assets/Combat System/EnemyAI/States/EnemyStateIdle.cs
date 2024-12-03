using UnityEngine;

public class EnemyStateIdle : FsmState
{
    private readonly IEnemyAI enemyAi;
    private readonly EnemyAISettings enemySettings;

    private readonly float idleStateMaxTime;
    private float idleStateTimer;

    public EnemyStateIdle(IEnemyAI enemyAi, Fsm stateMachine) : base(stateMachine)
    {
        this.enemyAi = enemyAi;
        enemySettings = enemyAi.AiSettings;
        
        idleStateMaxTime = enemySettings.idleTime;
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
        RoamingStateTransition();
    }

    private void RoamingStateTransition()
    {
        idleStateTimer -= Time.deltaTime;
        if (idleStateTimer <= 0)
            StateMachine.SetState<EnemyStateRoaming>();
    }

    public override void Exit()
    {
        Debug.Log("Idle State: [EXIT]");
    }
}