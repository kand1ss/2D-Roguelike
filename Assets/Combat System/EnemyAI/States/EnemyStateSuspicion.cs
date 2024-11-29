using UnityEngine;

public class EnemyStateSuspicion : FsmState
{
    private readonly IEnemyAI enemy;
    private readonly Player player;
    
    private Vector3 playerLastPos;

    private readonly float initSpeed;
    private readonly float speed;

    public EnemyStateSuspicion(IEnemyAI enemy, Fsm stateMachine, Player player, float speed) : base(stateMachine)
    {
        this.enemy = enemy;
        this.player = player;
        this.speed = speed;

        initSpeed = enemy.Agent.speed;
    }

    public override void Enter()
    {
        Debug.Log("Suspicion State: [ENTER]");
        enemy.Agent.speed = speed;
        
        playerLastPos = player.transform.position;
        enemy.Agent.SetDestination(playerLastPos);
    }

    public override void Update()
    {
        if (Vector3.Distance(enemy.transform.position, playerLastPos) <= 0.5f)
            StateMachine.SetState<EnemyStateIdle>();
    }

    public override void Exit()
    {
        enemy.Agent.speed = initSpeed;
    }
}