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
        AlertNearbyEnemies();
    }

    private void SetDestinationToPlayer()
    {
        playerLastPos = player.transform.position;
        enemyAI.Agent.SetDestination(playerLastPos);
    }

    private void AlertNearbyEnemies()
    {
        var hitColliders = GetEnemiesByRadius();

        foreach (var hitCollider in hitColliders)
        {
            EnemyAI allertedEnemyAi = hitCollider.GetComponent<EnemyAI>();

            if (allertedEnemyAi != null && allertedEnemyAi != (EnemyAI)enemyAI)
                allertedEnemyAi.SetState<EnemyStateSuspicion>();
        }
    }

    private Collider2D[] GetEnemiesByRadius()
    {
        var alertRadius = 6.5f;
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
        
        Collider2D[] hitColliders = 
            Physics2D.OverlapCircleAll(enemyAI.transform.position, alertRadius, enemyLayerMask);
        
        return hitColliders;
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