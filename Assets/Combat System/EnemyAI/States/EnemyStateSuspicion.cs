using UnityEngine;

public class EnemyStateSuspicion : FsmState
{
    private readonly IEnemyAI enemyAI;
    private readonly EnemyAISettings enemySettings;
    
    private readonly Player player;
    
    private Vector3 playerLastPos;

    private readonly float initSpeed;
    private readonly float speed;

    public EnemyStateSuspicion(IEnemyAI enemyAi, Fsm stateMachine, Player player, float speed) : base(stateMachine)
    {
        enemyAI = enemyAi;
        enemySettings = enemyAi.AiSettings;
        
        this.player = player;
        this.speed = speed;

        initSpeed = enemyAi.Agent.speed;
    }

    public override void Enter()
    {
        Debug.LogWarning("Suspicion State: [ENTER]");
        enemyAI.Agent.speed = speed;

        if (enemyAI is ICharacter enemyCharacter)
            enemyCharacter.StatsManager.OnTakeDamage += SetDestinationToPlayer;
        
        SetDestinationToPlayer();
    }

    private void SetDestinationToPlayer()
    {
        playerLastPos = player.transform.position;
        enemyAI.Agent.SetDestination(playerLastPos);
    }

    public override void Update()
    {
        IdleStateTransition();
    }

    private void IdleStateTransition()
    {
        if (Vector3.Distance(enemyAI.transform.position, playerLastPos) <= 0.5f)
            StateMachine.SetState<EnemyStateIdle>();
    }

    public override void Exit()
    {
        enemyAI.Agent.speed = initSpeed;
        
        if (enemyAI is ICharacter enemyCharacter)
            enemyCharacter.StatsManager.OnTakeDamage -= SetDestinationToPlayer;
    }
}